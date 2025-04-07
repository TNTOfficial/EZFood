
using EZFood.Domain.Entities.Models;

namespace EZFood.Infrastructure.Persistence.Interfaces;

public interface ICuisineTypeRepository : IReposioryBase<CuisineType>
{
    Task<IEnumerable<CuisineType>>GetAllCuisineTypesAsync();
    Task<CuisineType?> GetCuisineTypeByIdAsync(Guid id);
     void CreateCuisineTypeAsync(CuisineType packType);
    void UpdateCuisineTypeAsync(CuisineType packType);
    Task<bool> DeleteCuisineTypeAsync(Guid id);
    Task<bool> CuisineTypeExistsAsync(Guid id);
    Task<IEnumerable<CuisineType>> GetActiveCuisineTypesAsync();

}
