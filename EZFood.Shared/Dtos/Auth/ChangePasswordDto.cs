﻿namespace EZFood.Shared.Dtos.Auth;
public class ChangePasswordDto
{
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}

