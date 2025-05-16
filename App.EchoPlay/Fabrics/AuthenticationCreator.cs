using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;
using Infrastructure.EchoPlay.Authentications;
using Microsoft.AspNetCore.Http;

namespace App.EchoPlay.Fabrics;

public class AuthenticationCreator(IHttpContextAccessor accessor) : ICreator<IAuthentication<User>,AuthType>
{
    public string URL { get; set; }
    public IAuthentication<User> Create(AuthType type)
    {
        return type switch
        {
            AuthType.Cookie => new CookieAuthentication(accessor),
            AuthType.JwtBearer => new JwtAuthentication(accessor),
            _ => throw new ArgumentException($"Invalid authentication type: {type}")
        };
    }
}