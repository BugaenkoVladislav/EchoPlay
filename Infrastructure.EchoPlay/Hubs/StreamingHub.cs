using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class StreamingHub:Hub
{
    public async Task SendMessageForAllUsers(string senderId, byte[] streamData)
    {
        //возможно другой формат
        await Clients.All.SendAsync("ReceiveFrameForAllUsers", senderId, streamData);
    }
}