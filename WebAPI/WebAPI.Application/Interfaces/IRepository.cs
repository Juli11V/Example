using System.Linq.Expressions;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces;
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
    Task<IEnumerable<T>> GetWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications, bool trackChanges = false);
    Task<int> GetTotalCountWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications);
    Task<List<T>> GetPageWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications, int page, int pageSize);
    Task UpdateAsync(T entity);
}

