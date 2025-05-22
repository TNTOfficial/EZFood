using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EZFood.Application.Interfaces;
using EZFood.Shared.Dtos.Auth;
using EZFood.Shared.Dtos.User;
using EZFood.Shared.Exceptions;
using Newtonsoft.Json.Linq;
using EZFood.Domain.Entities.Models;

namespace EZFood.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto registrationDto)
    {
        if (registrationDto == null)
        {
            return BadRequest("Registration data is null");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        RegistrationResponseDto user = await _serviceManager.AuthService.RegisterUser(registrationDto, true);
        return Ok(user);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
           var (token, userDto) = await _serviceManager.AuthService.LoginAsync(loginRequest);
            TruckDetail? details = await _serviceManager.TruckDetailService.GetTruckDetailByUserAsync(userDto.Id);
            userDto.IsActive = details != null && details.IsActive;
            return Ok(new { success = true, user = userDto, token });
        }
        catch (Exception ex)
        {
            return Ok(new { success = false });
        }
        
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword)
    {
        string message= await _serviceManager.AuthService.ChangePasswordAsync(changePassword);
        return Ok(message);
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        string token = await _serviceManager.AuthService.GeneratePasswordResetTokenAsync(forgotPasswordDto);
        return Ok(new { token });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
    {
        await _serviceManager.AuthService.ResetPasswordAsync(resetPassword);
        return NoContent();
    }
}
