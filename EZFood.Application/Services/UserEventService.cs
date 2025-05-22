
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.CuisineType;
using EZFood.Shared.Dtos.Response;
using EZFood.Shared.Dtos.UserEvent;
using Microsoft.AspNetCore.Http;

namespace EZFood.Application.Services;

public class UserEventService : IUserEventService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid _userId = Guid.Empty;
    

    public UserEventService(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _httpContextAccessor = httpContextAccessor;

        string? userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
        if (userId != null)
        {
            _userId = Guid.Parse(userId);
        }
    }

    public Task<IEnumerable<UserEvent>> GetAllUserEventsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEvent>> GetUserEventsByUserIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto> UpdateUserEventsAsync(List<CreateUserEventDto> updateDto)
    {

        List<UserEvent> userEvents = [.. updateDto.Select(x => new UserEvent
        {
            Id = Guid.NewGuid(),
            EventId = x.EventId,
            UserId = _userId,
            Title = null,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            TimeZone = x.TimeZone,

        })];
        bool res = await _repositoryManager.UserEvent.UpdateUserEventsAsync(userEvents);
        if (res)
        {
            TruckDetail? truckDetail = await _repositoryManager.TruckDetail.getTruckDetailByUserAsync(_userId);
            if (truckDetail != null)
            {
                truckDetail.IsActive = true;
                truckDetail.Status = true;
                _repositoryManager.TruckDetail.Update(truckDetail);
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
                    Message = "User status could not be update. Please tru again."
                };
            }
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
