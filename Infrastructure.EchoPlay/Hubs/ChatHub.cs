using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class ChatHub:Hub,IChat
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
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