﻿using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Google.Apis.Auth.AspNetCore3;
using Infrastructure.EchoPlay.Authorizations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;

public class GoogleAuthentication(UnitOfWork uow, IEncryption encryption,IHttpContextAccessor accessor) : BaseAuthentication(uow, encryption,accessor)
{
    public override async Task AuthenticateAsync(User userData, long code)
    {
        await _accessor.HttpContext.ChallengeAsync("GoogleScheme",new AuthenticationProperties()
        {
            RedirectUri = "https://www.google.ru/?hl=ru"
        });
    }

    public override async Task UnauthenticateAsync(User userData)
    {
        await _accessor.HttpContext.SignOutAsync("GoogleScheme");
    }
}