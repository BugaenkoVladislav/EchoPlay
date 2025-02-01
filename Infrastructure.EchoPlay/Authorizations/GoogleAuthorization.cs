using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Authorizations;

public class GoogleAuthorization:BaseAuthorization
{
    public GoogleAuthorization(UnitOfWork uow,IEncryption encryption) : base(uow,encryption)
    {
    }
}