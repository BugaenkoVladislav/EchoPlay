using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class AuthService(AuthenticationCreator authenticationCreator, IAuthentication<User> authentication)
{
    private readonly AuthenticationCreator _authenticationCreator = authenticationCreator;
    private IAuthentication<User> _authentication = authentication;

    public async Task AuthenticateAsync(User user, AuthType authType, long code)
    {
        _authentication = _authenticationCreator.Create(authType);
        await _authentication.AuthenticateAsync(user, code);
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