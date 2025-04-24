using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Entities.Models;

public class TruckCuisineType
{
    [PrimaryKey]
    [Column(Order = 0)]
    [ForeignKey("TruckDetail")]
    public Guid TruckDetailId { get; set; }
    [PrimaryKey]
    [Column(Order = 1)]
    [ForeignKey("CuisineType")]
    public Guid CuisineTypeId { get; set; }
    [JsonIgnore]
    public virtual TruckDetail? TruckDetail { get; set; }
    [JsonIgnore]
    public virtual CuisineType? CuisineType { get; set; }
    [JsonIgnore]
    public virtual ICollection<TruckCuisineType>? CuisineTrucks { get; set; }

}
