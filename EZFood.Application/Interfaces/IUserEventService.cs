
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.UserEvent;

namespace EZFood.Application.Interfaces;

public interface IUserEventService
{
    Task<IEnumerable<UserEvent>> GetAllUserEventsAsync();
    Task<IEnumerable<UserEvent>> GetUserEventsByUserIdAsync(Guid id);
    Task<ResponseDto> UpdateUserEventsAsync(List<CreateUserEventDto> updateDto);   
}
