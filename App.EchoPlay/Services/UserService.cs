
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class UserService(UnitOfWork uow)
{
    private readonly UnitOfWork _uow = uow;

    public async Task AddUserPhoto(Guid userId, string photo)
    {
        var user = await _uow.UserRepository.GetEntityFirstAsync(x => x.Id == userId);
        user.PhotoPath = photo;
        await _uow.UserRepository.UpdateEntityFromExpressionAsync(user);
    }

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
}