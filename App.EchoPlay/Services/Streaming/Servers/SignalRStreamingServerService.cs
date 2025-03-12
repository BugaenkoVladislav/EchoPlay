using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services.Streaming.Servers;

public class SignalRStreamingServerService:Hub
{
    public async Task SendMessageForAllUsers(Guid user, byte[] message)
    {
        //логика стриминга возможно надо поменять на MediaFrameDto
        await Clients.All.SendAsync("ReceiveFromServerForAllUsers", user, message);
    }
}