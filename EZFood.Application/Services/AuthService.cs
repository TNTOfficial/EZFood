using Microsoft.AspNetCore.Identity;
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Constants;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.Auth;
using EZFood.Shared.Dtos.User;
using System.Security.Cryptography;
using System.Transactions;

namespace EZFood.Application.Services;
public class AuthService(IRepositoryManager repositoryManager,UserManager<ApplicationUser> userManager,
      ITokenService tokenService, IEmailService emailService) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEmailService _emailService = emailService;


    public async Task<string> LoginAsync(LoginRequestDto loginRequest)
    {
         (ApplicationUser appUser,User userProfile) = await GetUserAsync(loginRequest.Email);

        if (!await _userManager.CheckPasswordAsync(appUser, loginRequest.Password))
            throw new EZFoodException("Invalid credentials");

        return await _tokenService.GenerateJwtTokenAsync(appUser, userProfile);
    }

    public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto registrationDto)
    {

        // Use a transaction scope to ensure all operations succeed or fail together
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            string userName = registrationDto.Email;

            // 1. Create identity user
            ApplicationUser identityUser = new()
            {
                UserName = userName,
                Email = registrationDto.Email,
                PhoneNumber = registrationDto.PhoneNumber,
                PhoneNumberConfirmed = true,
            };

            IdentityResult? identityResult = await _userManager.CreateAsync(identityUser, registrationDto.Password);
            if (!identityResult.Succeeded)
            {
                throw new EZFoodException("Failed to create user account",
                         identityResult.Errors.Select(e => e.Description).ToArray());
            }

            // 2. Assign User Role
            await _userManager.AddToRoleAsync(identityUser, Roles.User);

            // 3. Create user profile
            User user = CreateUserEntity(registrationDto, identityUser.Id);
            _repositoryManager.User.Create(user);
            await _repositoryManager.SaveAsync();


            transactionScope.Complete();

            //// Send credentials to the user (instead of console.writeline)
            //await _notificationService.SendUserCredentials(user.PhoneNumber, user.Email, user.UserCode, password);

            return new RegistrationResponseDto
            {
                Email = user.Email,
                Message = "User Registered Successfully!"
            };
        }
        catch (Exception ex)
        {
          
            throw new EZFoodException("Registration Failed", new[] { ex.Message });
        }
    }

    public async Task<string> ChangePasswordAsync(ChangePasswordDto changePassword)
    {
         (ApplicationUser appUser, _) = await GetUserAsync(changePassword.Email);

        IdentityResult result = await _userManager.ChangePasswordAsync(
            appUser,
            changePassword.CurrentPassword,
            changePassword.NewPassword);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Description);
        return "Password changed successfully.";
    }

    public async Task<string> GeneratePasswordResetTokenAsync(ForgotPasswordDto forgotPasswordDto)
    {
         (ApplicationUser appUser, _) = await GetUserAsync(forgotPasswordDto.Email);

        // Generate password reset token
          string token =  await _userManager.GeneratePasswordResetTokenAsync(appUser);

        // Getting frontendUrl from forgotPasswordDto
        string frontendUrl = forgotPasswordDto.FrontendUrl ?? "https://myveddan.com/reset-password";

        // Send the email with token
        await _emailService.SendPasswordResetEmailAsync(
            appUser.Email!,
            token,
            frontendUrl);

        return "Check your email for the reset token";
    }

    public async Task ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
         (ApplicationUser appUser, _) = await GetUserAsync(resetPassword.Email);

        IdentityResult result = await _userManager.ResetPasswordAsync(
            appUser,
            resetPassword.Token,
            resetPassword.NewPassword);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Description);
    }

    private async Task<(ApplicationUser appUser, User userProfile)> GetUserAsync(string email)
    {
        User userProfile = await _repositoryManager.User.GetByEmail(email, false)
            ?? throw new NotFoundException("User not found");

        ApplicationUser appUser = await _userManager.FindByIdAsync(userProfile.IdentityUserId.ToString())
            ?? throw new NotFoundException("User not found");

        return (appUser, userProfile);
    }

    private static User CreateUserEntity(UserForRegistrationDto registrationDto, Guid identityUserId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            IdentityUserId = identityUserId,
            Status = true,
            CreatedAt = DateTime.UtcNow,

            // Map required fields with null checks
            Name = registrationDto.Name ?? string.Empty,
            Email = registrationDto.Email ?? string.Empty,
            PhoneNumber = registrationDto.PhoneNumber ?? string.Empty,            
            Address = registrationDto.Address ?? string.Empty,
        };       

        return user;
    }

    public string GenerateTemporaryPassword()
    {
        Random random = new Random();
        return random.Next(10000, 99999).ToString();
    }

}