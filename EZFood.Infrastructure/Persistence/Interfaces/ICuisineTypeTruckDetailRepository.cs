
using EZFood.Domain.Entities.Models;

namespace EZFood.Infrastructure.Persistence.Interfaces;

public interface ICuisineTypeTruckDetailRepository : IReposioryBase<CuisineTypeTruckDetail>
{
    Task<bool> DeleteRecordAsync(Guid TruckDetailid, Guid CuisineTypeId);
    Task<bool> DeleteRecordsAsync(Guid TruckDetailid);

}
