

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

    public async Task<IEnumerable<UserEvent>> GetUserEventsByIdAsync(Guid id)
    {
        return await FindByCondition(s => s.UserId == id && s.StartDate > DateTime.UtcNow, trackChanges: false).ToListAsync();
    }

    public async Task<bool> UpdateUserEventsAsync(List<UserEvent> events)
    {
        events.ForEach(x =>
        {
            UserEvent? event1 = _context.UserEvents.Where(t => t.EventId == x.EventId && t.UserId == x.UserId).FirstOrDefault();
            if (event1 == null)
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
