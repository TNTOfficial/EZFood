

using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;

namespace EZFood.Infrastructure.Persistence;

public class OnboardingActionRepository(EZFoodContext context) : RepositoryBase<OnboardingAction>(context), IOnboardingActionRepository
{

    public async Task<IEnumerable<OnboardingAction>> GetAllActionAsync()
    {
        return await FindAll(trackChanges: false).ToListAsync();
    }

    public async Task<IEnumerable<OnboardingAction>> GetAllActionByOnboardingAsync(Guid id)
    {
        return await FindByCondition(s => s.TruckDetailId == id, trackChanges: false).ToListAsync();
    }

    public async Task<OnboardingAction?> GetActionByIdAsync(Guid id)
    {
        return await FindByCondition(s => s.Id == id, trackChanges: false).SingleOrDefaultAsync();
    }

    public void CreateActionAsync(OnboardingAction packType)
    {
        Create(packType);
    }

    public void UpdateActionAsync(OnboardingAction packType)
    {
        Update(packType);
    }    
}
