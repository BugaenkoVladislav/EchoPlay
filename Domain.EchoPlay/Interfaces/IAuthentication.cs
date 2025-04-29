using Domain.EchoPlay.Enums;

namespace Domain.EchoPlay.Interfaces;

public interface IAuthentication<in TUserData>
{
    long GenerateCode();
    Task IdentifyUser(TUserData userData);
    Task AuthenticateAsync(TUserData userData,long code);
    
    Task UnauthenticateAsync(TUserData userData);
    Task<bool> Check2FaAsync(TUserData userData, long code);

}