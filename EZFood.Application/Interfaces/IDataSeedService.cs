using EZFood.Domain.Entities.Models;

namespace EZFood.Application.Interfaces;

public interface IDataSeedService
{
    Task<User?> SeedAdminUserAsync();
}