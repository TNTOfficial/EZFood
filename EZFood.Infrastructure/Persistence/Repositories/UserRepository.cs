using Microsoft.EntityFrameworkCore;
using EZFood.Domain.Entities.Models;
using EZFood.Infrastructure.Persistence.DbContext;
using EZFood.Infrastructure.Persistence.Interfaces;
using EZFood.Infrastructure.Persistance.Repositories;

namespace EZFood.Infrastructure.Persistence.Repositories;

public class UserRepository(EZFoodContext context):RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> GetById(Guid id, bool trackChanges) =>
        await FindByCondition(u => u.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();

    public async Task<User?> GetByPhoneNumber(string phoneNumber, bool trackChanges) =>
        await FindByCondition(u => u.PhoneNumber.Equals(phoneNumber), trackChanges)
            .FirstOrDefaultAsync();

    public async Task<User?> GetByEmail(string email, bool trackChanges) =>
        await FindByCondition(u => u.Email!.Equals(email), trackChanges)
            .FirstOrDefaultAsync();

    public async Task<bool> Exists(string phoneNumber, string email) =>
        await FindByCondition(u =>
            u.PhoneNumber.Equals(phoneNumber) ||
            u.Email!.Equals(email), false)
        .AnyAsync();

    public async Task<IEnumerable<User>> GetAllUsers(bool trackChanges) =>
        await FindAll(trackChanges)
             .ToListAsync();
    public async Task<int> GetTotalUsersCount() =>
          await FindAll(false).CountAsync();

    public void AttachAndMarkModified(User entity)
    {
        _context.Set<User>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
