namespace EZFood.Shared.Dtos.User;
public class UserForRegistrationDto
{
    public required string Name { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = null!;
}
