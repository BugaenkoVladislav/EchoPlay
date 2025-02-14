using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;

namespace App.EchoPlay.Services;

public class AuthService(IAuthenticationCreator authenticationCreator)
{
    private readonly IAuthenticationCreator _authenticationCreator = authenticationCreator;
    private IAuthentication<User> _authentication;
    public async Task AuthenticateAsync(User user, AuthType authType)
    {
        _authentication = _authenticationCreator.CreateAuthentication(authType);
        await _authentication.AuthenticateAsync(user);
    }

    public async Task UnauthenticateAsync(User user, AuthType authType)
    {
        await _authentication.UnauthenticateAsync(user);
    }
}