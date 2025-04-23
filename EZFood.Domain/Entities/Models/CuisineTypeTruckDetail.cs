using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class CuisineTypeTruckDetail
{
    [ForeignKey("CuisineTypesId")]
    public Guid CuisineTypesId { get; set; }
    public virtual CuisineType? CuisineType { get; set; }
    [ForeignKey("TruckDetailsId")]
    public Guid TruckDetailsId { get; set; }
    public virtual TruckDetail? TruckDetail { get; set; }


}
