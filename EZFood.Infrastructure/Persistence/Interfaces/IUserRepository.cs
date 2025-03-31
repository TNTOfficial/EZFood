using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.Interfaces;

namespace EZFood.Infrastructure.Persistence.Interfaces;
public interface IUserRepository : IReposioryBase<User>
{
    Task<User?> GetById(Guid id, bool trackChanges);
    Task<User?> GetByPhoneNumber(string phoneNumber, bool trackChanges);
    Task<User?> GetByEmail(string email, bool trackChanges);
    Task<bool> Exists(string phoneNumber, string email);
    Task<IEnumerable<User>> GetAllUsers(bool trackChanges);
    Task<int> GetTotalUsersCount();
    void AttachAndMarkModified(User entity);
}
