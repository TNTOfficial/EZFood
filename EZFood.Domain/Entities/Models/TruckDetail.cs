using EZFood.Domain.Entities.Enums;
using EZFood.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class TruckDetail
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for truck name is 60 characters.")]
    public required string TruckName { get; set; }

    [MaxLength(30, ErrorMessage = "Maximum length for truck owner name is 30 characters.")]
    public required string TruckOwnerName { get; set; }
    [MaxLength(10, ErrorMessage = "maximum length for phone number is 10 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(10, ErrorMessage = "maximum length for phone number is 15 characters")]
    public string? WhatsappNumber { get; set; }
    [MaxLength(50, ErrorMessage = "maximum length for email name is 50 characters")]
    public required string BusinessEmail { get; set; }
    [MaxLength(200, ErrorMessage = "maximum length for address name is 200 characters")]
    public required string Address { get; set; }
    public bool IsOtherCuisine { get; set; } = false;

    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? CuisineNote { get; set; }
    [MaxLength(500, ErrorMessage = "Maximum length for Description is 500 characters.")]
    public string? BusinessDescription { get; set; }
    public int? BussinessStartYear { get; set; }
    [MaxLength(15, ErrorMessage = "maximum length for EIN is 15 characters")]
    public string? EIN { get; set; }
    public bool IsBreakfast { get; set; } = false;
    public bool IsLunch { get; set; } = false;
    public bool IsDinner { get; set; } = false;
    [Column(TypeName = "decimal(7, 2)")]
    public Decimal MinimumGuaranteeAmount { get; set; } = 0;
    [MaxLength(200)]
    public string? COI { get; set; }
    [MaxLength(200)]
    public string? W9 { get; set; }
    [MaxLength(200)]
    public string? ServeSafeCertificate { get; set; }
    [MaxLength(200)]
    public string? DCHCertificate { get; set; }
    public string? BannerUrl { get; set; }
    [JsonIgnore]
    public string ImageJson { get; set; } = string.Empty;

    [JsonIgnore]
    public string MenuJson { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public OnboardingStatus OnboardingStatus { get; set; } = OnboardingStatus.Pending;
    [MaxLength(500, ErrorMessage = "Maximum length for onboarding note is 500 characters.")]
    public string? OnboardingNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
    public List<CuisineType> CuisineTypes { get; set; } = [];
    [NotMapped]
    public virtual List<string>? ImageList
    {
        get => JsonOptions.ListData(ImageJson);
        set => ImageJson = value != null ? JsonOptions.ListDataObject<List<string>>(value) : string.Empty;
    }

    [NotMapped]
    public virtual List<string>? MenuList
    {
        get => JsonOptions.ListData(MenuJson);
        set => MenuJson = value != null ? JsonOptions.ListDataObject<List<string>>(value) : string.Empty;
    }


}
