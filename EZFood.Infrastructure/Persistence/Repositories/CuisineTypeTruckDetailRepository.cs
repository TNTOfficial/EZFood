

using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;
using System.Security.AccessControl;
using EZFood.Domain.Entities.Enums;

namespace EZFood.Infrastructure.Persistence;

public class CuisineTypeTruckDetailRepository(EZFoodContext context) : RepositoryBase<CuisineTypeTruckDetail>(context), ICuisineTypeTruckDetailRepository
{

  
    public async Task<bool> DeleteRecordAsync(Guid TruckDetailid, Guid CuisineTypeId )
    {
        CuisineTypeTruckDetail? record = await FindByCondition(s => s.CuisineTypesId == CuisineTypeId && s.TruckDetailsId == TruckDetailid, trackChanges: false)
            .SingleOrDefaultAsync();
        if (record == null)
            return false;
        Delete(record);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRecordsAsync(Guid id)
    {
        List<CuisineTypeTruckDetail> records = await FindByCondition(s => s.TruckDetailsId == id, trackChanges: false).ToListAsync();
        
        DeleteMany(records);
        await _context.SaveChangesAsync();
        return true;
    }


}
