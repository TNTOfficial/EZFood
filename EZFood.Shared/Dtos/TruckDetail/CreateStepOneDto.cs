

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateStepOneDto
{
    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for truck name is 60 characters.")]
    public required string TruckName { get; set; }

    [MaxLength(30, ErrorMessage = "Maximum length for truck owner name is 30 characters.")]
    public required string TruckOwnerName { get; set; }
    [MaxLength(10, ErrorMessage = "maximum length for phone number is 10 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(50, ErrorMessage = "maximum length for email name is 50 characters")]
    public required string BusinessEmail { get; set; }
    [MaxLength(200, ErrorMessage = "maximum length for address name is 200 characters")]
    public required string Address { get; set; }
}
