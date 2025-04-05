using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class StreamingHub:Hub
{
    public async Task SendMessageForAllUsers(string senderId, byte[] streamData)
    {
        await Clients.All.SendAsync("ReceiveFrameForAllUsers", senderId, streamData);
    }
}