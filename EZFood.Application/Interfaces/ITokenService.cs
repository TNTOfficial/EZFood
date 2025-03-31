using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Identity;

namespace EZFood.Application.Interfaces;
public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser appUser, User userProfile);
}

