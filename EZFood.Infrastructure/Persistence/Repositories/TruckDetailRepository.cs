

using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;
using System.Security.AccessControl;
using EZFood.Domain.Entities.Enums;

namespace EZFood.Infrastructure.Persistence;

public class TruckDetailRepository(EZFoodContext context) : RepositoryBase<TruckDetail>(context), ITruckDetailRepository
{

    public async Task<IEnumerable<TruckDetail>> GetAllTruckDetailsAsync()
    {
        return await FindAll(trackChanges: false).ToListAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetPendingTruckDetaisAsync()
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus == OnboardingStatus.Pending).ToListAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetRejectedTruckDetaisAsync()
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus == OnboardingStatus.Rejected).ToListAsync();
    }


    public async Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id)
    {
        return await FindByCondition(s => s.Id == id, trackChanges: false).SingleOrDefaultAsync();
    }

    public async Task<TruckDetail?> getTruckDetailByUserAsync(Guid id)
    {
        return await FindByCondition(s => s.UserId == id, trackChanges: false).SingleOrDefaultAsync();
    }

    public void CreateTruckDetailAsync(TruckDetail truckDetail)
    {
        Create(truckDetail);
    }

    public void UpdateTruckDetailAsync(TruckDetail truckDetail)
    {
        Update(truckDetail);
    }

    public async Task<bool> DeleteTruckDetailAsync(Guid id)
    {
        TruckDetail? truckDetail = await FindByCondition(s => s.Id == id, trackChanges: false)
            .SingleOrDefaultAsync();
        if (truckDetail == null)
            return false;
        Delete(truckDetail);
        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> TruckDetailExistsAsync(Guid id)
    {
        return await FindByCondition(s => s.Id == id, trackChanges: false).AnyAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetActiveTruckDetaisAsync()
    {
        return await FindByCondition(s => s.OnboardingStatus == OnboardingStatus.Approved, trackChanges: false)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

}
