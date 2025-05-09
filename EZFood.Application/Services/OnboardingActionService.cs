
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.OnboardingAction;
using EZFood.Shared.Dtos.Response;

namespace EZFood.Application.Services;

public class OnboardingActionService(IRepositoryManager repositoryManager) : IOnboardingActionService
{

    private readonly IRepositoryManager _repositoryManager = repositoryManager;


    public async Task<IEnumerable<OnboardingAction>> GetAllActionsByIdAsync(Guid id)
    {
        return await _repositoryManager.OnboardingAction.GetAllActionByOnboardingAsync(id);
    }

    public Task<OnboardingAction?> GetActionByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto> CreateActionAsync(CreateOnboardingActionDto createActionDto)
    {

        TruckDetail? truckDetail = await _repositoryManager.TruckDetail.GetTruckDetailForUpdateByIdAsync(createActionDto.TruckDetailId);
        if (truckDetail != null)
        {
            OnboardingAction action = new OnboardingAction
            {
                Id = Guid.NewGuid(),
                TruckDetailId = createActionDto.TruckDetailId,
                Note = createActionDto.Note,
                OnboardingStatus = createActionDto.OnboardingStatus
            };
            _repositoryManager.OnboardingAction.CreateActionAsync(action);
            await _repositoryManager.SaveAsync();
            truckDetail.OnboardingStatus = createActionDto.OnboardingStatus;
            _repositoryManager.TruckDetail.UpdateTruckDetailAsync(truckDetail);
            await _repositoryManager.SaveAsync();
            return new ResponseDto { Result = true, Message = "Response has been submitted successfully" };
        } else
        {
            return new ResponseDto { Result = false, Message = "Could not find onboarding details." };
        }
    }


    public async Task<IEnumerable<CuisineType>> GetAllCuisineTypesAsync()
    {
        return await _repositoryManager.CuisineType.GetAllCuisineTypesAsync();
    }

    public async  Task<CuisineType?> GetCuisineTypeByIdAsync(Guid id)
    {
        CuisineType? cuisineType = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (cuisineType == null)
        {
            throw new EZFoodException("Cuisine type id not found.");
        }
        return cuisineType;
    }

    public async Task<CuisineType> CreateCuisineTypeAsync(CreateCuisineTypeDto createCuisineTypeDto)
    {

        CuisineType type = new CuisineType
        {
            Id = Guid.NewGuid(),
            Name = createCuisineTypeDto.Name,
            Description = createCuisineTypeDto.Description
        };
        _repositoryManager.CuisineType.CreateCuisineTypeAsync(type);
        await _repositoryManager.SaveAsync();
        return type;
    }
    public async Task<CuisineType?> UpdateCuisineTypeAsync(Guid id, UpdateCuisineTypeDto updateCuisineTypeDto)
    {
        CuisineType? existingType = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if(existingType == null)
        {
            return null;
        }

        CuisineType cuisineType = new()
        {
            Id = id,
            Name = updateCuisineTypeDto.Name,
            Description = updateCuisineTypeDto.Description,
            Status = updateCuisineTypeDto.Status,
        };
        _repositoryManager.CuisineType.Update(cuisineType);
        await _repositoryManager.SaveAsync();
        return cuisineType;
    }

   

    public async Task<bool> DeleteCuisineTypeAsync(Guid id)
    {
        CuisineType? type = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (type == null)
            return false;
        return await _repositoryManager.CuisineType.DeleteCuisineTypeAsync(id);
    }
   


    public async Task<IEnumerable<CuisineType>> GetActiveCuisineTypesAsync()
    {
        return await _repositoryManager.CuisineType.GetActiveCuisineTypesAsync();
    }

    public async Task<bool> UpdateCuisineTypeStatusAsync(Guid id, bool status)
    {
        CuisineType? type = await _repositoryManager.CuisineType.GetCuisineTypeByIdAsync(id);
        if (type == null)
        {
            return false;
        }
        type.Status = status;
        _repositoryManager.CuisineType.UpdateCuisineTypeAsync(type);
        await _repositoryManager.SaveAsync();
        return true;
    }

};
