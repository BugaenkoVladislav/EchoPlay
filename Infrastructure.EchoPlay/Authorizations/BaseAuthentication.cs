using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EchoPlay.Authorizations;

public class BaseAuthentication:IAuthentication<User>
{
    protected UnitOfWork _uow;
    protected IEncryption _encryption;
    protected IHttpContextAccessor _accessor;
    public BaseAuthentication(UnitOfWork uow,IEncryption encryption,IHttpContextAccessor accessor)
    {
        _uow = uow;
        _encryption = encryption;
        _accessor = accessor;
    }
    
    public virtual async Task AuthenticateAsync(User userData)
    {
        try
        {
            //ref create
            userData.Email = await _encryption.DecryptAsync(userData.Email);
            userData.Username = await _encryption.DecryptAsync(userData.Username);
            userData.Password = await _encryption.DecryptAsync(userData.Password);
            var user = await _uow.UserRepository.GetEntityFirstAsync(x => x.Username == userData.Username && x.Password == userData.Password);
            if (user.Is2FA)
            {
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
        catch (NullReferenceException)
        {
            //когда пользователь пустой
        }
        catch (Exception ex)
        {
            
        }
        
    }

    public async Task UnauthenticateAsync(User userData)
    {
        try
        {
            await AuthenticateAsync(userData);
            //remove logic 
        }
        catch (Exception ex)
        {
            
        }
    }

    public async Task<bool> Check2FaAsync(long code)
    {
        try
        {
            //todo if sent code realy - return true
        }
        catch (Exception ex)
        {
            
        }
        throw new NotImplementedException();
    }
}