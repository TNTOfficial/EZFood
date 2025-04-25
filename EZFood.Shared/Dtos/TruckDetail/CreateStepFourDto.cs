

using EZFood.Shared.Dtos.TruckDetail;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateStepFourDto
{   
    public List<IFormFile>? Images { get; set; } = null!;
}
