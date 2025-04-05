using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services.Streaming;

public class SignalRServer(StreamingHub streamingHub):Hub,IStreamingServer
{
    private readonly StreamingHub _streamingHub = streamingHub;
    public async Task SendMessageForAllUsers( string userId,byte[] data)
    {
        await _streamingHub.SendMessageForAllUsers(userId, data);
    }
}