using System.Security.Claims;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Authorizations;

public class JwtAuthorization:BaseAuthorization
{
    private UnitOfWork _uow;
    private IEncryption _encryption;
    
    public JwtAuthorization(UnitOfWork uow,IEncryption encryption) : base(uow,encryption)
    {
        _uow = uow;
        _encryption = encryption;
    }

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