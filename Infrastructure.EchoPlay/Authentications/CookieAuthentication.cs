using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;

public class CookieAuthentication(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : BaseAuthentication(uow, encryption, accessor), IAuthentication<User>
{
    public override async Task AuthenticateAsync(User userData,long code)
    {
        await base.AuthenticateAsync(userData,code);
        var claims = new List<Claim> { new (ClaimTypes.Name, userData.Username) };
        var claimsIdentity = new ClaimsIdentity(claims);
        await _accessor.HttpContext.SignInAsync("CookieScheme", new ClaimsPrincipal(claimsIdentity));
    }

    public override async Task UnauthenticateAsync(User userData)
    {
        await _accessor.HttpContext.SignOutAsync("CookieScheme");
    }
}