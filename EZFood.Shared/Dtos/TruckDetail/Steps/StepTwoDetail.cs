using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail.Steps;

public class StepTwoDetail
{
    public string? CuisineNote { get; set; }
    public bool IsOtherCuisine { get; set; }
    public List<string> Cuisines { get; set; } = new();
}
