
using EZFood.Domain.Entities.Models;

namespace EZFood.Infrastructure.Persistence.Interfaces;

public interface IOnboardingActionRepository : IReposioryBase<OnboardingAction>
{
    Task<IEnumerable<OnboardingAction>>GetAllActionAsync();
    Task<IEnumerable<OnboardingAction>>GetAllActionByOnboardingAsync(Guid id);
    Task<OnboardingAction?> GetActionByIdAsync(Guid id);
     void CreateActionAsync(OnboardingAction packType);
    void UpdateActionAsync(OnboardingAction packType);

}
