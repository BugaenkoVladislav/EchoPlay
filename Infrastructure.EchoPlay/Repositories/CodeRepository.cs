using Domain.EchoPlay.Entities;

namespace Infrastructure.EchoPlay.Repositories;

public class CodeRepository(MyDbContext dbContext):BaseRepository<Code>(dbContext)
{
    private readonly MyDbContext _dbContext = dbContext;
}