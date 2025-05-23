﻿

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

    public async Task<IEnumerable<TruckDetail>> GetTruckDetailsForStatusAsync(OnboardingStatus status)
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus == status).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetTruckDetailsForIncompleteStatusAsync()
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus >= OnboardingStatus.Step1 && x.OnboardingStatus <= OnboardingStatus.Pending).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<int> GetTruckDetailsForStatusCountAsync(OnboardingStatus status)
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus == status).CountAsync();
    }

    public async Task<int> GetTruckDetailsForStatusIncompleteCountAsync()
    {
        return await FindAll(trackChanges: false).Where(x => x.OnboardingStatus >= OnboardingStatus.Step1  && x.OnboardingStatus <= OnboardingStatus.Pending).CountAsync();
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
        return await FindByCondition(s => s.Id == id, trackChanges: false).Include(x => x.CuisineTypes).SingleOrDefaultAsync();
    }


    public async Task<TruckDetail?> GetTruckDetailForUpdateByIdAsync(Guid id)
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

    public async Task<IEnumerable<TruckDetail>> GetActiveTruckDetailsAsync()
    {
        return await FindByCondition(s => s.IsActive && s.Status, trackChanges: false)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

}
