using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Authorizations;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Fabrics;

public class AuthenticationBuilder(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : IAuthenticationCreator
{
    public IAuthentication<User> CreateAuthentication(AuthType authType)
    {
        return authType switch
        {
            AuthType.Cookie => new CookieAuthentication(uow, encryption, accessor),
            AuthType.JwtBearer => new JwtAuthentication(uow, encryption, accessor),
            AuthType.Google => new GoogleAuthentication(uow, encryption, accessor),
            _ => throw new ArgumentException($"Invalid authentication type: {authType}")
        };
    }
}