using Microsoft.EntityFrameworkCore;
using EZFood.Infrastructure.Persistence.DbContext;
using System.Linq.Expressions;
using EZFood.Infrastructure.Persistence.Interfaces;

namespace EZFood.Infrastructure.Persistance.Repositories;
public abstract class RepositoryBase<T>(EZFoodContext context):IReposioryBase<T> where T : class
{
    protected EZFoodContext _context = context ?? throw new ArgumentNullException(nameof(context));
    public IQueryable<T> FindAll(bool trackChanges) =>
       !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

 
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
    }
    public void Create(T entity) => _context.Set<T>().Add(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public void DeleteMany(List<T> entities) => entities.ForEach(x => _context.Set<T>().Remove(x)); 
}
