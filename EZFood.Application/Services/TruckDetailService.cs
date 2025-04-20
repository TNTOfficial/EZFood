
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Domain.Entities.Enums;

namespace EZFood.Application.Services;

public class TruckDetailService(IRepositoryManager repositoryManager) : ITruckDetailService
{

    private readonly IRepositoryManager _repositoryManager = repositoryManager;


    public async Task<IEnumerable<TruckDetail>> GetAllTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetAllTruckDetailsAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetPendingTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetPendingTruckDetaisAsync();
    }

    public async Task<IEnumerable<TruckDetail>> GetApprovedTruckDetailsAsync()
    {
        return await _repositoryManager.TruckDetail.GetAllTruckDetailsAsync();
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


    

    public Task<TruckDetail?> GetTruckDetailByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TruckDetail> CreateTruckDetailAsync(CreateTruckDetailDto createPackTypeDto)
    {
        throw new NotImplementedException();
    }

    public Task<TruckDetail?> UpdateTruckDetailAsync(Guid id, UpdateTruckDetailDto updatePackTypeDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTruckDetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOnboardingStatusAsync(Guid id, OnboardingStatus onboardingStatus)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTruckDetailStatusAsync(Guid id, bool status)
    {
        throw new NotImplementedException();
    }
};
