using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;
public class BaseAuthentication(IHttpContextAccessor accessor) 
{
    protected IHttpContextAccessor _accessor = accessor;
}