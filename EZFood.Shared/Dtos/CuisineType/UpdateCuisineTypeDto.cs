using System.ComponentModel.DataAnnotations;

namespace EZFood.Shared.Dtos.CuisineType; 
public class UpdateCuisineTypeDto
{
    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for Name is 30 characters.")]
    public required string Name { get; set; }

    [MaxLength(200, ErrorMessage = "Maximum length for description is 200 characters")]
    public string? Description { get; set; }

    public bool Status { get; set; }
}
