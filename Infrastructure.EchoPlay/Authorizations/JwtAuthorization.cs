using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public class JwtAuthorization(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : BaseAuthorization(uow, encryption, accessor)
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