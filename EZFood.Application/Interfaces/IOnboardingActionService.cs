
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.OnboardingAction;
using EZFood.Shared.Dtos.Response;

namespace EZFood.Application.Interfaces;

public interface IOnboardingActionService
{
    Task<IEnumerable<OnboardingAction>> GetAllActionsByIdAsync(Guid id);
    Task<OnboardingAction?> GetActionByIdAsync(Guid id);
    Task<ResponseDto> CreateActionAsync(CreateOnboardingActionDto createActionDto);
    
}
