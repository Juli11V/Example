using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Linq.Expressions;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Data;
namespace WebAPI.Application.Services;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
    }
    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<T>> GetWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications, bool trackChanges = false)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var specification in specifications)
        {
            query = query.Where(specification.ToExpression());
        }
        return await query.ToListAsync();
    }
    public async Task<int> GetTotalCountWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var specification in specifications)
        {
            query = query.Where(specification.ToExpression());
        }
        return await query.CountAsync();
    }
    public async Task<List<T>> GetPageWithSpecificationsAsync(IEnumerable<ISpecification<T>> specifications, int page, int pageSize)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var specification in specifications)
        {
            query = query.Where(specification.ToExpression());
        }
        return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

}

