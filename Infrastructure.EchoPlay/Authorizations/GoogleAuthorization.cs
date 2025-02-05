using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public class GoogleAuthorization(UnitOfWork uow, IEncryption encryption,IHttpContextAccessor accessor) : BaseAuthorization(uow, encryption,accessor)
{
    public override async Task AuthenticateAsync(User userData)
    {
        await base.AuthenticateAsync(userData);
        await _accessor.HttpContext.ChallengeAsync("GoogleScheme",new AuthenticationProperties()
        {
            RedirectUri = "https://www.google.ru/?hl=ru"
        });
    }
}