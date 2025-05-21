

using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;

namespace EZFood.Infrastructure.Persistence;

public class UserEventRepository(EZFoodContext context) : RepositoryBase<UserEvent>(context), IUserEventRepository
{
    public Task<IEnumerable<UserEvent>> GetAllUserEventsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEvent>> GetUserEventsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserEventsAsync(List<UserEvent> events)
    {
        events.ForEach(async x =>
        {
            if (!await EventExistsByEventIdAsync(x.EventId!, x.UserId))
            {
                Create(x);
            }
        });
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> EventExistsByEventIdAsync(string id, Guid userId)
    {
        return await FindByCondition(s => s.UserId == userId && s.EventId == id, trackChanges: false).AnyAsync();
    }
}
