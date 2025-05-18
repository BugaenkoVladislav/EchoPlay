using Domain.EchoPlay.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EchoPlay.Repositories;

public class TmpUserRepository(MyDbContext dbContext) : BaseRepository<TmpUser>(dbContext)

{
    
}