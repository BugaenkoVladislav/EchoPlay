using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;

namespace App.EchoPlay.Services;

public class AuthService
{
    private readonly IAuthorizationCreator _authorizationCreator;

    public AuthService(IAuthorizationCreator authorizationCreator)
    {
        _authorizationCreator = authorizationCreator;
    }

    public async Task AuthenticateAsync(User user, AuthType authType)
    {
        var authorization = _authorizationCreator.CreateAuthorization(authType);
        await authorization.AuthenticateAsync(user);
    }
}