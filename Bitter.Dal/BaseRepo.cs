using Bitter.Dal.Interfaces;
using Bitter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitter.Dal;

public abstract class BaseRepo<T> : IRepo<T> where T : class
{
    protected BitterDbContext Db { get; }
    protected DbSet<T> Table { get; }
    
    protected BaseRepo(BitterDbContext dbContext)
    {
        Db = dbContext;
        Table = dbContext.Set<T>();
    }
    
    public async Task<T?> GetOneAsync(object id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T> AddOneAsync(T entity)
    {
        Table.Add(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        Table.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Table.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await Db.SaveChangesAsync();
    }
}