using System.Reflection.Metadata;
using Core.Entities;

namespace Core.Interfaces;
public interface IGenericRepository<T> where T: BaseEntity
{    
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(int id);
    Task<bool> SaveChangesAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
     Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T,TResult> spec);
     Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);

}    
