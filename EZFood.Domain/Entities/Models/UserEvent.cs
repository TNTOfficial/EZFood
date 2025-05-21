using EZFood.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class UserEvent
{
    [Key]
    public Guid Id { get; set; }
    public string? EventId { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for title is 200 characters.")]
    public string? Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? TimeZone { get; set; }
    public EventType EventType { get; set; } = EventType.External;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool Status { get; set; } = true;
    public virtual User? User { get; set; }
}
