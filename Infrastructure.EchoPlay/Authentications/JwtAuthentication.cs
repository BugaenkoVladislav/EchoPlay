using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;

public class JwtAuthentication(IHttpContextAccessor accessor) : BaseAuthentication(accessor), IAuthentication<User>
{
    public Task AuthenticateAsync(User userData)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userData.Username),
            new Claim(ClaimTypes.Email, userData.Email),
        };
        return Task.FromResult("");
    }

    public Task UnauthenticateAsync()
    {
        throw new NotImplementedException();
    }
}