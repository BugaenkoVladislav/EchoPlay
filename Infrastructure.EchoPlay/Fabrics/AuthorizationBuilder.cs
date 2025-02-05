using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Authorizations;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Fabrics;

public class AuthorizationBuilder:IAuthorizationCreator
{
    private readonly UnitOfWork _uow;
    private readonly IEncryption _encryption;
    private readonly IHttpContextAccessor _accessor;

    public AuthorizationBuilder(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor)
    {
        _uow = uow;
        _encryption = encryption;
        _accessor = accessor;
    }
    
    public IAuthorization<User> CreateAuthorization(AuthType authType)
    {
        return authType switch
        {
            AuthType.Cookie => new CookieAuthorization(_uow, _encryption, _accessor),
            AuthType.JwtBearer => new JwtAuthorization(_uow, _encryption, _accessor),
            AuthType.Google => new GoogleAuthorization(_uow, _encryption, _accessor),
            _ => throw new ArgumentException($"Invalid authentication type: {authType}")
        };
    }
}