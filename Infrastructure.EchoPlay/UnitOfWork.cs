using System.Reflection;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Repositories;

namespace Infrastructure.EchoPlay;

public class UnitOfWork(MyDbContext context, UserRepository userRepository, MessageRepository messageRepository)
{
    public IRepository<User> UserRepository { get; set; } = userRepository;
    public IRepository<Message> MessageRepository { get; set; } = messageRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await context.DisposeAsync();
    }
}