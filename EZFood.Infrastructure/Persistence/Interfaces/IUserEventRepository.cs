
using EZFood.Domain.Entities.Models;

namespace EZFood.Infrastructure.Persistence.Interfaces;

public interface IUserEventRepository : IReposioryBase<UserEvent>
{
    Task<IEnumerable<UserEvent>>GetAllUserEventsAsync();
    Task<IEnumerable<UserEvent>> GetUserEventsByIdAsync(Guid id);
    Task<bool> UpdateUserEventsAsync(List<UserEvent> events);

}
