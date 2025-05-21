namespace EZFood.Shared.Dtos.User;

// Base DTO with common properties
public class UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool Status { get; set; }
    public bool IsActive { get; set; }
}
