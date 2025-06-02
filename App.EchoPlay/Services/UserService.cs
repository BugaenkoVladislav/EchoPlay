
using Domain.EchoPlay.Entities;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class UserService(UnitOfWork uow)
{
    private readonly UnitOfWork _uow = uow;
    
    public async Task<string> GetUserPhoto(Guid userId)
    {
        var user = await _uow.UserRepository.GetEntityFirstAsync(x => x.Id == userId);
        return user.PhotoPath;
    }

    public async Task<Guid> GetUserId(string username)
    {
        var user = await _uow.UserRepository.GetEntityFirstAsync(x => x.Username == username || x.Email == username);
        return user.Id;
    }

    public async Task UpdateUser(User user)
    {
        await _uow.UserRepository.UpdateEntityAsync(user);
        await _uow.SaveChangesAsync();
    }
}