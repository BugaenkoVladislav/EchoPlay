using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Hubs;

namespace App.EchoPlay.Fabrics;

public class StreamingCreator(string roomName):ICreator<IStreamingServer,StreamingType>
{
    private readonly string _roomName = roomName;
    public IStreamingServer Create(StreamingType type)
    {
        return type switch
        {
            StreamingType.SignalR => new StreamingHub(_roomName),
            //other types
            _ => throw new ArgumentException($"Invalid streaming type: {type}")
        };
    }
}