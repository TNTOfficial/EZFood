

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateTruckDetailDto
{

    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for truck name is 60 characters.")]
    public required string TruckName { get; set; }

    [MaxLength(30, ErrorMessage = "Maximum length for truck owner name is 30 characters.")]
    public required string TruckOwnerName { get; set; }
    [MaxLength(10, ErrorMessage = "maximum length for phone number is 10 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(10, ErrorMessage = "maximum length for whatsapp phone number is 10 characters")]
    public string WhatsappNumber { get; set; } = string.Empty;
    [MaxLength(50, ErrorMessage = "maximum length for email name is 50 characters")]
    public required string BusinessEmail { get; set; }
    [MaxLength(200, ErrorMessage = "maximum length for address name is 200 characters")]
    public required string Address { get; set; }

    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? CuisineNote { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? BusinessDescription { get; set; }
    public int BussinessStartYear { get; set; }
    [MaxLength(15, ErrorMessage = "maximum length for EIN is 15 characters")]
    [Required]
    public required string EIN { get; set; }
    public bool IsBreakfast { get; set; } = false;
    public bool IsLunch { get; set; } = false;
    public bool IsDinner { get; set; } = false;
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
    public string? ImageJson { get; set; }

    [JsonIgnore]
    public string? MenuJson { get; set; }

}
