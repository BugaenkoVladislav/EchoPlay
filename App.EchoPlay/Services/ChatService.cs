using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services;

public class ChatService:Hub,IChat
{
    public async Task SendMessage(string message)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMessage(Guid messageId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMessage(Guid messageId)
    {
        throw new NotImplementedException();
    }
}