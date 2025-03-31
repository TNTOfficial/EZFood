using System.ComponentModel.DataAnnotations;

namespace EZFood.Domain.Entities.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdentityUserId { get; set; }
    [MaxLength(30, ErrorMessage = "maximum length for user name is 30 characters")]
    public required string Name { get; set; }
    [MaxLength(50, ErrorMessage = "maximum length for email name is 50 characters")]
    public required string Email { get; set; }
    [MaxLength(10, ErrorMessage = "maximum length for phone number is 10 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
