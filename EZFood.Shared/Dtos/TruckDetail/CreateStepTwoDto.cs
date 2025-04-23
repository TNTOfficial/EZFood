

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateStepTwoDto
{
    [MaxLength(200, ErrorMessage = "Maximum length for truck name is 60 characters.")]
    public string? CuisineNote { get; set; }
    public bool IsOtherCuisine { get; set; }
    public List<Guid> Cuisines { get; set; } = new();

}
