using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class CuisineType
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(30, ErrorMessage = "Maximum length for Name is 60 characters.")]
    public required string Name { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for Description is 200 characters.")]
    public string? Description { get; set; }
    public bool Status { get; set; } = true;
    public List<TruckDetail> TruckDetails { get; } = [];

}
