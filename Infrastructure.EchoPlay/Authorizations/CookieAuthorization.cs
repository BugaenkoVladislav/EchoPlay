using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public class CookieAuthorization(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : BaseAuthorization(uow, encryption, accessor)
{
    public override async Task AuthenticateAsync(User userData)
    {
        await base.AuthenticateAsync(userData);
        var claims = new List<Claim> { new (ClaimTypes.Name, userData.Username) };
        var claimsIdentity = new ClaimsIdentity(claims);
        await _accessor.HttpContext.SignInAsync("CookieScheme", new ClaimsPrincipal(claimsIdentity));
    }
}