using System.Linq.Expressions;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class DatabaseService(UnitOfWork uow, MyDbContext dbContext)
{
    private readonly MyDbContext _dbContext = dbContext;

    public async Task<List<TEntity>?> GetValuesAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    {
        var repository = uow.GetRepoByEntity<TEntity>();
        return await repository.GetEntitiesAsync(expression);
    }

    public async Task<TEntity> GetDataAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    {
        var repository = uow.GetRepoByEntity<TEntity>();
        return await repository.GetEntityFirstAsync(expression);
    }

    public async Task AddDataAsync<TEntity>(TEntity entity)
    {
        var repository = uow.GetRepoByEntity<TEntity>();
        await repository.AddNewEntityAsync(entity);
    }

    public async Task UpdateDataAsync<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> expression)
    {
        var repository = uow.GetRepoByEntity<TEntity>();
        await repository.UpdateEntityFromExpressionAsync(entity, expression);
    }

    public async Task DeleteDataAsync<TEntity>(TEntity entity)
    {
        var repository = uow.GetRepoByEntity<TEntity>();
        await repository.DeleteEntityAsync(entity);
    }
}