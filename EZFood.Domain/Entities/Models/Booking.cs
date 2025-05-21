using EZFood.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class Booking
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? Description { get; set; }
    public DateTime EventStartTime { get; set; }
    public DateTime? EventEndTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    [ForeignKey("Seller")]
    public Guid? SellerId { get; set; }

    public bool Status { get; set; } = true;
    public virtual User? User { get; set; }
    public virtual User? Seller { get; set; }

}
