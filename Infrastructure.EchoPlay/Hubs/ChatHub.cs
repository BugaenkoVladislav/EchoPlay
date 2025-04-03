using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class ChatHub : Hub, IChat
{
    public async Task SendMessage(string message) => await Clients.All.SendAsync("ReceiveMessage", message);
    
    public async Task SendPrivateMessage(string connectionId, string message) => await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", message);

    public async Task UpdateMessage(string messageId, string message) => await Clients.All.SendAsync("UpdateMessage", messageId, message);
    
    public async Task DeleteMessage(string messageId) => await Clients.All.SendAsync("DeleteMessage", messageId);
}