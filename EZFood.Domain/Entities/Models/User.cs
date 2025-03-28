using System.ComponentModel.DataAnnotations;

namespace EZFood.Domain.Entities.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public Guid IdentityUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
