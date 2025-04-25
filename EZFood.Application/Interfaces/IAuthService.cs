using EZFood.Shared.Dtos.Auth;
using EZFood.Shared.Dtos.User;

namespace EZFood.Application.Interfaces;

public interface IAuthService
{
    Task<(string token, UserDto userDto)> LoginAsync(LoginRequestDto loginRequest);
    Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userDto, Boolean isSeller = false);
    Task<string> ChangePasswordAsync(ChangePasswordDto changePassword);
    Task ResetPasswordAsync(ResetPasswordDto resetPassword);
    Task<string> GeneratePasswordResetTokenAsync(ForgotPasswordDto forgotPasswordDto);
}
