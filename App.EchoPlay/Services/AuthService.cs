using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;

namespace App.EchoPlay.Services;

public class AuthService(IAuthenticationCreator authenticationCreator)
{
    private readonly IAuthenticationCreator _authenticationCreator = authenticationCreator;
    private IAuthentication<User> _authentication;
    public async Task AuthenticateAsync(User user, AuthType authType,long code)
    {
        _authentication = _authenticationCreator.CreateAuthentication(authType);
        await _authentication.AuthenticateAsync(user,code);
    }

    public async Task UnauthenticateAsync(User user)
    {
        await _authentication.UnauthenticateAsync(user);
    }

    public async Task IdentifyUserAsync(User user)
    {
        await _authentication.IdentifyUser(user);
    }
}