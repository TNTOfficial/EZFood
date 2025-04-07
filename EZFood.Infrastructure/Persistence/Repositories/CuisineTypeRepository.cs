

using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;

namespace EZFood.Infrastructure.Persistence;

public class CuisineTypeRepository(EZFoodContext context) : RepositoryBase<CuisineType>(context), ICuisineTypeRepository
{
    public async Task<IEnumerable<CuisineType>> GetAllCuisineTypesAsync()
    {
        return await FindAll(trackChanges: false).ToListAsync();
    }

    public async Task<CuisineType?> GetCuisineTypeByIdAsync(Guid id)
    {
        return await FindByCondition(s => s.Id == id, trackChanges: false).SingleOrDefaultAsync();
    }



    public void CreateCuisineTypeAsync(CuisineType type)
    {
        Create(type);
    }

    public void UpdateCuisineTypeAsync(CuisineType packType)
    {
        Update(packType);
    }
    public async Task<bool> DeleteCuisineTypeAsync(Guid id)
    {
        CuisineType? cuisineType = await FindByCondition(s => s.Id == id, trackChanges: false)
            .SingleOrDefaultAsync();
        if (cuisineType == null)
            return false;
        Delete(cuisineType);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> CuisineTypeExistsAsync(Guid id)
    {
        return await FindByCondition(s => s.Id == id, trackChanges: false).AnyAsync();
    }

    public async Task<IEnumerable<CuisineType>> GetActiveCuisineTypesAsync()
    {
        return await FindByCondition(s => s.Status, trackChanges: false)
            .OrderByDescending(s => s.Name)
            .ToListAsync();
    }

}
