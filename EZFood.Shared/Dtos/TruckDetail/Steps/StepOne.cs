using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail.Steps;

public class StepOne
{
    public string? TruckName { get; set; }
    public string? TruckOwnerName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BusinessEmail { get; set; }
    public string? Address { get; set; }
}
