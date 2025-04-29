using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;

namespace Domain.EchoPlay.Interfaces;

public interface ICreator<TInterface,TType>
{
    string URL { get; set; }
    TInterface Create(TType type);
}