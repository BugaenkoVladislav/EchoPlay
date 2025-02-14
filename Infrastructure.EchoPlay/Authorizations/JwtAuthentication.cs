using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public class JwtAuthentication(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : BaseAuthentication(uow, encryption, accessor)
{
    public override async Task<string> AuthenticateAsync(User userData)
    {
        await base.AuthenticateAsync(userData);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userData.Username),
            new Claim(ClaimTypes.Email, userData.Email),
        };
        return "";
    }
}