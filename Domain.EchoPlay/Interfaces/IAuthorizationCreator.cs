using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;

namespace Domain.EchoPlay.Interfaces;

public interface IAuthorizationCreator
{
    IAuthorization<User> CreateAuthorization(AuthType authType);
}