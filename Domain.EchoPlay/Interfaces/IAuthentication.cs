using Domain.EchoPlay.Enums;

namespace Domain.EchoPlay.Interfaces;

public interface IAuthentication<in TUserData>
{
    Task AuthenticateAsync(TUserData userData);
    
    Task UnauthenticateAsync(TUserData userData);
    
    Task<bool> Check2FaAsync(long code);
}