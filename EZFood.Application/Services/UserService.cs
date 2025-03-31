

using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Shared.Dtos.Common;
using EZFood.Shared.Dtos.User;
using EZFood.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EZFood.Application.Services;

public class UserService( IRepositoryManager repositoryManager, UserManager<ApplicationUser> userManager
   ) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<UserDetailDto> GetUserDetailById(Guid id)
    {
      User user = await _repositoryManager.User.GetById(id, false)
            ?? throw new KeyNotFoundException($"User with ID {id} not found");
        return  MapToUserDetailDtoForEdit(user);
    }

    public async Task<IEnumerable<UserDto>> SearchUsersAsync(string searchQuery)
    {
        try
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return [];
            }

            string normalizedQuery = searchQuery.ToLower().Trim();
            IEnumerable<User> users = await _repositoryManager.User.FindByCondition(
                u => u.Name.ToLower().Contains(normalizedQuery) ||
                     u.Email!.ToLower().Contains(normalizedQuery) ||
                     u.PhoneNumber.Contains(normalizedQuery),
                false)
                .ToListAsync();

            // Manually map User to UserDto
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
            });
        }
        catch (Exception ex)
        {
            throw new EZFoodException($"No user found, {ex.Message}");
        }
    }


   
    public async Task<PagedResponse<UserDetailDto>> GetUsersAsync(UserParameters parameters)
    {
        try
        {

            IQueryable<User> usersQuery = _repositoryManager.User.FindAll(false)                
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string normalizedQuery = parameters.SearchTerm.ToLower().Trim();
                usersQuery = usersQuery.Where(
                    u => u.Name.ToLower().Contains(normalizedQuery) ||
                    u.Email!.ToLower().Contains(normalizedQuery) ||
                    u.PhoneNumber.ToLower().Contains(normalizedQuery));
            }

            usersQuery = ApplySorting(usersQuery, parameters.SortBy, parameters.SortDirection);
            int totalCount = await usersQuery.CountAsync();
            var pagedUsers = await usersQuery
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            var userDtos = pagedUsers.Select(MapToUserDetailDto).ToList();
            return new PagedResponse<UserDetailDto>
            {
                Items = userDtos,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)parameters.PageSize)
            };

        }
        catch (Exception ex)
        {
            throw new EZFoodException($"Error retrieving users: {ex.Message}");
        }
    }

    private UserDetailDto MapToUserDetailDto(User user)
    {
        return new UserDetailDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            Status = user.Status,
            Address = user.Address,
            CompanyName = user.CompanyName,
            CreatedAt = user.CreatedAt,            
        };
    }

    private UserDetailDto MapToUserDetailDtoForEdit(User user)
    {
        return new UserDetailDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status,
            ImageUrl = user.ImageUrl,
            Address = user.Address,
            CompanyName = user.CompanyName,
            CreatedAt = user.CreatedAt,            
        };
    }
    private IQueryable<User> ApplySorting(IQueryable<User> query, string sortBy, string sortDirection)
    {
        Expression<Func<User, object>> keySelector = sortBy?.ToLower() switch
        {
            "name" => u => u.Name,
            "email" => u => u.Email!,
            "phonenumber" => u => u.PhoneNumber,
            "createdat" => u => u.CreatedAt,
            "status" => u => u.Status,
            _ => u => u.CreatedAt // Default sort
        };

        return sortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);
    }
}
