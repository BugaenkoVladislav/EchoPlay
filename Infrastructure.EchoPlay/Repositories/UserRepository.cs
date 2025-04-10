using Domain.EchoPlay.Entities;

namespace Infrastructure.EchoPlay.Repositories;

public class UserRepository(MyDbContext dbContext) :BaseRepository<User>(dbContext)
{
    private readonly MyDbContext _dbContext = dbContext;
}