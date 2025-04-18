using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Hubs;

namespace App.EchoPlay.Fabrics;

public class StreamingCreator:ICreator<IStreamingServer,StreamingType>
{
    public string URL { get; set; }
    public IStreamingServer Create(StreamingType type)
    {
        return type switch
        {
            StreamingType.SignalR => new StreamingHub(URL),
            //other types
            _ => throw new ArgumentException($"Invalid streaming type: {type}")
        };
    }
}