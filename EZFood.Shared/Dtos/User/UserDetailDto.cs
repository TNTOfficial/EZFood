using EZFood.Shared.Dtos.User;

namespace EZFood.Shared.Dtos.User;

// Detailed DTO with full user information
public class UserDetailDto:UserDto
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
