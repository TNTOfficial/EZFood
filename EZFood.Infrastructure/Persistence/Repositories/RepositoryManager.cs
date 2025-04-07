using Microsoft.EntityFrameworkCore;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistence.Repositories;

namespace EZFood.Infrastructure.Persistence;

public sealed class RepositoryManager(EZFoodContext context):IRepositoryManager
{
    private readonly EZFoodContext _context = context;
    private readonly Lazy<IUserRepository> userRepository = new(() => new UserRepository(context));
    private readonly Lazy<ICuisineTypeRepository> cuisineTypeRepository = new(() => new CuisineTypeRepository(context));


    public IUserRepository User => userRepository.Value;
    public ICuisineTypeRepository CuisineType => cuisineTypeRepository.Value;

    public async Task<T> ExecuteScalarAsync<T>(string sql)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;

        var result = await command.ExecuteScalarAsync();
        return (T)Convert.ChangeType(result, typeof(T))!;
    }
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
