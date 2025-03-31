using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EZFood.Application.Interfaces;
using EZFood.Application.Services;
using EZFood.Domain.Entities.Models;
using EZFood.Shared.Constants;
using EZFood.Shared.Dtos;
using EZFood.Shared.Dtos.Common;
using EZFood.Shared.Dtos.User;
using System.Security.Claims;

namespace EZFood.Presentation.Controllers;
[ApiController]
[Route("api/users")]
public class UsersController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;



    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        UserDetailDto user = await _serviceManager.UserService.GetUserDetailById(id);
        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters parameters)
    {
        PagedResponse<UserDetailDto> result = await _serviceManager.UserService.GetUsersAsync(parameters);
        return Ok(result);
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> SearchUsers([FromQuery] string query)
    {
        IEnumerable<UserDto> users = await _serviceManager.UserService.SearchUsersAsync(query);
        return Ok(users);
    }

    //[HttpPut("{userId:guid}")]
    //[Authorize]
    //public async Task<IActionResult> UpdateUser(Guid userId,[FromBody] UserForUpdateDto userUpdateDto)
    //{
     
    //    UserDetailDto updatedUser = await _serviceManager.UserService.UpdateUserAsync(userId, userUpdateDto);
    //    return Ok(updatedUser);
    //}     



    private Guid GetCurrentUserId()
    {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            throw new UnauthorizedAccessException("Invalid user identity");
        }
        return userId;
    }
}