using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Authorizations;

public class CookieAuthorization:BaseAuthorization
{
    public CookieAuthorization(UnitOfWork uow,IEncryption encryption) : base(uow,encryption)
    {
    }
}