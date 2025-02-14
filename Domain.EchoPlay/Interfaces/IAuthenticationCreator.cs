using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;

namespace Domain.EchoPlay.Interfaces;

public interface IAuthenticationCreator
{
    IAuthentication<User> CreateAuthentication(AuthType authType);
}