
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;

namespace EZFood.Application.Services;

public class CuisineTypeService(IRepositoryManager repositoryManager) : ICuisineTypeService
{

    private readonly IRepositoryManager _repositoryManager = repositoryManager;
   
   
 

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
