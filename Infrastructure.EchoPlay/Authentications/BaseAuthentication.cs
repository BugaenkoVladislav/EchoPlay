using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authentications;
//todo реструктурировать чтобы UOW находился в сервисе а не в реализации и тогда BaseAuth вообще не будет нужен
public class BaseAuthentication(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor)
    : IAuthentication<User>
{
    protected UnitOfWork _uow = uow;
    protected IEncryption _encryption = encryption;
    protected IHttpContextAccessor _accessor = accessor;

    public long GenerateCode()
    {
        var rnd = new Random();
        var code = rnd.Next(100000, 999999);
        return code;
    }

    public async Task IdentifyUser(User userData)
    {
        try
        {
            //userData.Email = await _encryption.DecryptAsync(userData.Email);
            //userData.Username = await _encryption.DecryptAsync(userData.Username);
            //userData.Password = await _encryption.DecryptAsync(userData.Password);
            var user = await _uow.UserRepository.GetEntityFirstAsync(x =>
                x.Username == userData.Username && x.Password == userData.Password);

            var code = GenerateCode();
            if (user.Phone is not null)
            {
                //todo SendCodeOnPhone
            }
            else
            {
                //todo SendCodeOnEmail
            }
            //add code to db
        }
        catch (InvalidOperationException ex)
        {
            //когда пользователь пустой
        }
        catch (Exception ex)
        {
        }
    }

    public virtual async Task AuthenticateAsync(User userData, long code)
    {
        try
        {
            if (!await Check2FaAsync(userData, code))
                throw new UnauthorizedAccessException();
        }
        catch (UnauthorizedAccessException ex)
        {
        }
        catch (Exception ex)
        {
        }
    }


    public virtual async Task UnauthenticateAsync(User userData)
    {
    }

    public async Task<bool> Check2FaAsync(User userData, long code)
    {
        try
        {
            //todo if sent code realy - Authenticate
            //return false;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}