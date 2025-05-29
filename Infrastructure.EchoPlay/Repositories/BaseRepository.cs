using System.Linq.Expressions;
using Domain.EchoPlay.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EchoPlay.Repositories;

public class BaseRepository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity:class
{
    public virtual async Task<TEntity?>GetEntityFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
    }
    public virtual async Task<TEntity>GetEntityFirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbContext.Set<TEntity>().FirstAsync(expression);
    }
    public virtual async Task<List<TEntity>?> GetEntitiesAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbContext.Set<TEntity>().Where(expression).ToListAsync();
    }

    public virtual async Task AddNewEntityAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task DeleteEntityAsync(TEntity entity)
    {
        await Task.Run(() => dbContext.Set<TEntity>().Remove(entity));
    }

    public virtual async Task UpdateEntityFromExpressionAsync(TEntity entity)
    {
        await Task.Run(() => dbContext.Set<TEntity>().Update(entity));
    }
}