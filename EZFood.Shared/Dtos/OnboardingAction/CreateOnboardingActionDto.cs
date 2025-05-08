

using EZFood.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.OnboardingAction;

public class CreateOnboardingActionDto
{
    public Guid TruckDetailId { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? Note { get; set; }
    public OnboardingStatus OnboardingStatus { get; set; } = OnboardingStatus.ReferBack;
}
