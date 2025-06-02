using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;

public class CookieAuthentication(IHttpContextAccessor accessor) : BaseAuthentication(accessor), IAuthentication<User>
{
    public async Task AuthenticateAsync(User userData)
    {
        var claims = new List<Claim> { 
            new (ClaimTypes.Email, userData.Email), 
            new (ClaimTypes.Name, userData.Username)};
        if (!string.IsNullOrEmpty(userData.PhotoPath))
        {
            claims.Add(new (ClaimTypes.Actor, userData.PhotoPath));
        }
        
        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        await _accessor.HttpContext.SignInAsync("CookieScheme", new ClaimsPrincipal(claimsIdentity));
    }

    public  async Task UnauthenticateAsync()
    {
        await _accessor.HttpContext.SignOutAsync("CookieScheme");
    }
}