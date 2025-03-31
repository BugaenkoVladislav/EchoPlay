using App.EchoPlay.Dtos;
using Infrastructure.EchoPlay.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services.Streaming;

public class SignalRServerService(StreamingHub streamingHub):Hub
{
    private readonly StreamingHub _streamingHub = streamingHub;
    public async Task SendMessageForAllUsers(MediaFrameDto frame)
    {
        await _streamingHub.SendMessageForAllUsers(frame.SenderId, frame.Data);
    }
}