namespace EZFood.Infrastructure.Persistence.Interfaces;
public interface IRepositoryManager
{
    Task<T> ExecuteScalarAsync<T>(string sql);
    IUserRepository User { get; }
    Task SaveAsync();
}

