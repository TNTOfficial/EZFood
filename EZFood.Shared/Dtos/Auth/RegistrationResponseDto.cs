﻿namespace EZFood.Shared.Dtos.Auth;
public class RegistrationResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = "Registration successful";
}