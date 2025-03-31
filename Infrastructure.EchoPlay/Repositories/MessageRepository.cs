using System.Linq.Expressions;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Hubs;

namespace Infrastructure.EchoPlay.Repositories;

public class MessageRepository(MyDbContext dbContext):BaseRepository<Message>(dbContext)
{
    private readonly MyDbContext _dbContext = dbContext;
}