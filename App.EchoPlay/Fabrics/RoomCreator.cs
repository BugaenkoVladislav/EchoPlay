using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Hubs;

namespace App.EchoPlay.Fabrics;

public class RoomCreator:ICreator<IRoom,RoomTypes>
{
    public string URL { get; set; }

    public IRoom Create(RoomTypes type)
    { 
        return type switch
        {
            RoomTypes.SignalR => new RoomHub(URL),
            //other types
            _ => throw new ArgumentException($"Invalid streaming type: {type}")
        };
    }
}
