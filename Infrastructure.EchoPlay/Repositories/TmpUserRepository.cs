using Domain.EchoPlay.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EchoPlay.Repositories;

public class TmpUserRepository(DbContext dbContext) : BaseRepository<TmpUser>(dbContext)

{
    
}