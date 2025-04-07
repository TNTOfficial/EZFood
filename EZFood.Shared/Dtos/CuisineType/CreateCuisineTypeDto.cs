

using System.ComponentModel.DataAnnotations;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateCuisineTypeDto
{

    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for Name is 60 characters.")]
    public required string Name { get; set; }
    [MaxLength(200, ErrorMessage = "Maximum length for description is 200 characters")]
    public string? Description { get; set; }

}
