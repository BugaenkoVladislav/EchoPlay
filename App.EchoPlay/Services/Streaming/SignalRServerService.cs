using App.EchoPlay.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services.Streaming;

public class SignalRServerService:Hub
{
    public async Task SendMessageForAllUsers(MediaFrameDto frame)
    {
        //возможно другой формат
        await Clients.All.SendAsync("ReceiveFrameForAllUsers", frame.SenderId, frame.Data);
    }
}