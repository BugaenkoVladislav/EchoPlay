using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public abstract class BaseAuthentication(UnitOfWork uow, IEncryption encryption, IHttpContextAccessor accessor) : IAuthentication<User>
{
    protected UnitOfWork _uow = uow;
    protected IEncryption _encryption = encryption;
    protected IHttpContextAccessor _accessor = accessor;

    public async Task IdentifyUser(User userData)
    {
        try
        {
            userData.Email = await _encryption.DecryptAsync(userData.Email);
            userData.Username = await _encryption.DecryptAsync(userData.Username);
            userData.Password = await _encryption.DecryptAsync(userData.Password);
            var user = await _uow.UserRepository.GetEntityFirstAsync(x => x.Username == userData.Username && x.Password == userData.Password);
            if (user.Is2FA)
            {
                var code = GenerateCode();
                if (user.Phone is not null)
                {
                    //todo SendCodeOnPhone
                }
                else
                {
                    //todo SendCodeOnEmail
                }
            }
        }
        catch (NullReferenceException ex)
        {
            //когда пользователь пустой
        }
        catch (Exception ex)
        {
            
        }
    }

    public virtual async Task AuthenticateAsync(User userData,long code)
    {
        try
        {
            if (!await Check2FaAsync(code))
                throw new UnauthorizedAccessException();
        }
        catch (UnauthorizedAccessException ex)
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }

    public long GenerateCode()
    {
        var rnd = new Random();
        var code = rnd.Next(100000, 999999);
        return code;
    }

    public virtual async Task UnauthenticateAsync(User userData)
    {
    }

    public async Task<bool> Check2FaAsync(long code)
    {
        try
        {
            //todo if sent code realy - Authenticate
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
}