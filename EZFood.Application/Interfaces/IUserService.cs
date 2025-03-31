using EZFood.Domain.Entities.Models;
using EZFood.Shared.Dtos;
using EZFood.Shared.Dtos.Common;
using EZFood.Shared.Dtos.User;

namespace EZFood.Application.Interfaces;

public interface IUserService
{
    Task<UserDetailDto> GetUserDetailById(Guid id);
    Task<PagedResponse<UserDetailDto>>GetUsersAsync(UserParameters parameters);
    Task<IEnumerable<UserDto>> SearchUsersAsync(string searchQuery);

}
