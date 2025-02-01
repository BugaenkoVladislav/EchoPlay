using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Repositories;

namespace Infrastructure.EchoPlay;

public class UnitOfWork(MyDbContext context,UserRepository userRepository)
{
    public IRepository<User> UserRepository { get; set; } = userRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
        
}