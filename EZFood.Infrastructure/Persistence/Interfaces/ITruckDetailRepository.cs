
using EZFood.Domain.Entities.Enums;
using EZFood.Domain.Entities.Models;

namespace EZFood.Infrastructure.Persistence.Interfaces;

public interface ITruckDetailRepository : IReposioryBase<TruckDetail>
{
    Task<IEnumerable<TruckDetail>>GetAllTruckDetailsAsync();
    Task<IEnumerable<TruckDetail>> GetTruckDetailsForStatusAsync(OnboardingStatus status);
    Task<IEnumerable<TruckDetail>> GetTruckDetailsForIncompleteStatusAsync();
    Task<int> GetTruckDetailsForStatusCountAsync(OnboardingStatus status);
    Task<int> GetTruckDetailsForStatusIncompleteCountAsync();
    Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id);
    Task<TruckDetail?> GetTruckDetailForUpdateByIdAsync(Guid id);
    Task<TruckDetail?> getTruckDetailByUserAsync(Guid id);
    void CreateTruckDetailAsync(TruckDetail truckDetail);
    void UpdateTruckDetailAsync(TruckDetail truckDetail);
    Task<bool> DeleteTruckDetailAsync(Guid id);
    Task<bool> TruckDetailExistsAsync(Guid id);
    Task<IEnumerable<TruckDetail>> GetActiveTruckDetailsAsync();
    Task<IEnumerable<TruckDetail>> GetPendingTruckDetaisAsync();
    Task<IEnumerable<TruckDetail>> GetRejectedTruckDetaisAsync();

}
