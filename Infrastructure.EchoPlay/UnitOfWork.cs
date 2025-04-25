using System.Reflection;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Repositories;

namespace Infrastructure.EchoPlay;

public class UnitOfWork
{
    private readonly MyDbContext _context;

    public UnitOfWork(MyDbContext context, UserRepository userRepository, MessageRepository messageRepository)
    {
        _context = context;
        UserRepository = userRepository;
        MessageRepository = messageRepository;
        //Добавляем репозитории в словарь для работы с сервисом баз данных
        _repositories.Add(typeof(User), UserRepository);
        _repositories.Add(typeof(Message), MessageRepository);
    }


    public IRepository<User> UserRepository { get; set; } 
    public IRepository<Message> MessageRepository { get; set; } 

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public IRepository<TEntity> GetRepoByEntity<TEntity>()
    {
        if (_repositories[typeof(TEntity)] is not IRepository<TEntity> repo)
        {
            throw new TypeLoadException($"Repository with this type doesn't implement IRepository<{typeof(TEntity)}>");
        }
        return repo;
    } 
    

    private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
}