using System.Reflection;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Repositories;

namespace Infrastructure.EchoPlay;

public class UnitOfWork(MyDbContext context, UserRepository userRepository, MessageRepository messageRepository,CodeRepository codeRepository)
{
    public IRepository<User> UserRepository { get; set; } = userRepository;
    public IRepository<Message> MessageRepository { get; set; } = messageRepository;
    public IRepository<Code> CodeRepository { get; set; } = codeRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await context.DisposeAsync();
    }
}