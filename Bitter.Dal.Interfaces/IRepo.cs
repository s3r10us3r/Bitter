using Bitter.Shared.Models;

namespace Bitter.Dal.Interfaces;

public interface IRepo<T>
{
    Task<T?> GetOneAsync(object id);
    Task<List<T>> GetAllAsync();
    Task<T> AddOneAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
}