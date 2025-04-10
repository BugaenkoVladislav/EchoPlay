using System.Collections.Concurrent;
using System.Threading.Channels;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class StreamingHub(string roomName):Hub,IStreamingServer
{
    private readonly string _roomName = roomName;
    
    public async Task SendMessageForAllUsers(ChannelReader<byte[]> stream,string userId)
    {
        while (await stream.WaitToReadAsync())
        {
            while (stream.TryRead(out var data))
            {
                await Clients.Group(_roomName).SendAsync("ReceiveFrameForAllUsers", userId, data);
            }
        }
    }
}