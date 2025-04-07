
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;

namespace EZFood.Application.Interfaces;

public interface ICuisineTypeService
{
    Task<IEnumerable<CuisineType>> GetAllCuisineTypesAsync();
    Task<CuisineType?> GetCuisineTypeByIdAsync(Guid id);
    Task<CuisineType> CreateCuisineTypeAsync(CreateCuisineTypeDto createPackTypeDto);
    Task<CuisineType?> UpdateCuisineTypeAsync(Guid id, UpdateCuisineTypeDto updatePackTypeDto);
    Task<bool> DeleteCuisineTypeAsync(Guid id);
    Task<bool> UpdateCuisineTypeStatusAsync(Guid id, bool status);
    Task<IEnumerable<CuisineType>> GetActiveCuisineTypesAsync();
}
