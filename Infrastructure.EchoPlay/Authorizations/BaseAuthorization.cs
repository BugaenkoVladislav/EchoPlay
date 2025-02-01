using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Authorizations;

public class BaseAuthorization:IAuthorization<User>
{
    private UnitOfWork _uow;
    private IEncryption _encryption;
    public BaseAuthorization(UnitOfWork uow,IEncryption encryption)
    {
        _uow = uow;
        _encryption = encryption;
    }
    
    public virtual async Task AuthenticateAsync(User userData)
    {
        try
        {
            //ref create
            userData.Email = await _encryption.DecryptAsync(userData.Email);
            userData.Username = await _encryption.DecryptAsync(userData.Username);
            userData.Password = await _encryption.DecryptAsync(userData.Password);
            var user = await _uow.UserRepository.GetEntityFirstOrDefaultAsync(x => x.Username == userData.Username && x.Password == userData.Password);
            if (user is null)
                throw new UnauthorizedAccessException();
            if (user.Phone is not null)
            {
                //todo SendCodeOnPhone
            }
            else
            {
                //todo SendCodeOnEmail
            }
        }
        catch (Exception ex)
        {
            
        }
        
    }

    public async Task UnauthenticateAsync(User userData)
    {
        try
        {

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