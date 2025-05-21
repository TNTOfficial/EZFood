
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.UserEvent;

namespace EZFood.Application.Services;

public class UserEventService(IRepositoryManager repositoryManager) : IUserEventService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public Task<IEnumerable<UserEvent>> GetAllUserEventsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEvent>> GetUserEventsByUserIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto> UpdateUserEventsAsync(UpdateUserEventDto updateDto)
    {

        List<UserEvent> userEvents = [.. updateDto.UserEvents.Select(x => new UserEvent
        {
            Id = Guid.NewGuid(),
            EventId = x.EventId,
            UserId = updateDto.Id,
            Title = null,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            TimeZone = x.TimeZone,

        })];
        bool res = await _repositoryManager.UserEvent.UpdateUserEventsAsync(userEvents);
        if (res)
        {
            return new ResponseDto
            {
                Result = true,
                Message = "Events synced successfully."

            };
        } else
        {
            return new ResponseDto
            {
                Result = false,
                Message = "Events couldn't be synced successfully."

            };
        }
    }
};
