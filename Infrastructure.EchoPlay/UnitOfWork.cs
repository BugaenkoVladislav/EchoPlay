using System.Reflection;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Repositories;

namespace Infrastructure.EchoPlay;

public class UnitOfWork(MyDbContext context, UserRepository userRepository,CodeRepository codeRepository,TmpUserRepository tmpUserRepository)
{
    public IRepository<User> UserRepository { get; set; } = userRepository;
    public IRepository<Code> CodeRepository { get; set; } = codeRepository;
    public IRepository<TmpUser> TmpUserRepository { get; set; } = tmpUserRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await context.DisposeAsync();
    }
}