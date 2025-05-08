using EZFood.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class OnboardingAction
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("TruckDetail")]
    public Guid TruckDetailId { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? Note { get; set; }
    public OnboardingStatus OnboardingStatus { get; set; } = OnboardingStatus.ReferBack;
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public virtual TruckDetail? TruckDetail { get; set; }

}
