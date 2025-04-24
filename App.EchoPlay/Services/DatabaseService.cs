using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;
using Infrastructure.EchoPlay.Repositories;

namespace App.EchoPlay.Services;
//todo дописчать этот метод
public class DatabaseService
{
    private readonly UnitOfWork _uow;
    private readonly MyDbContext _dbContext;
    public DatabaseService(UnitOfWork uow, MyDbContext dbContext)
    {
        _uow = uow;
        _dbContext = dbContext;
    }

    // public async Task<List<TEntity>> GetDataAsync<TEntity>()
    // {
    //     
    // }
    //
    // public async Task<Domain.EchoPlay.Entities.User> GetDataAsync(Guid userId)
    // {
    //     
    // }
    public async Task AddDataAsync(Domain.EchoPlay.Entities.User userId)
    {
        
    }
    
    public async Task UpdateDataAsync(Guid userId ,Domain.EchoPlay.Entities.User user)
    {
        
    }
    
    public async Task DeleteDataAsync(Guid userId)
    {
        
    }
    
}