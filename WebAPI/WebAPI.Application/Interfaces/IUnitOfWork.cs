using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
    Task<IDisposable> BeginTransactionAsync();
    Task CommitAsync(IDisposable transaction);
    Task RollbackAsync(IDisposable transaction);
    Task UpdateAsync<T>(T entity) where T : class;
}

