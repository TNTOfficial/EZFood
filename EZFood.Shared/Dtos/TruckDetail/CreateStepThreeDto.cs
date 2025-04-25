

using EZFood.Shared.Dtos.TruckDetail;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Shared.Dtos.CuisineType;

public class CreateStepThreeDto
{
    public required string BusinessDescription { get; set; }
    public int BussinessStartYear { get; set; }
    public required string EIN { get; set; }
    public bool IsBreakfast { get; set; }
    public bool IsLunch { get; set; }
    public bool IsDinner { get; set; }
    public Decimal MinimumGuaranteeAmount { get; set; } = 0;
    public IFormFile? COI { get; set; } = null!;
    public IFormFile? W9 { get; set; } = null!;
    public IFormFile? ServeSafeCertificate { get; set; } = null!;
    public IFormFile? DCHCertificate { get; set; } = null!;

}
