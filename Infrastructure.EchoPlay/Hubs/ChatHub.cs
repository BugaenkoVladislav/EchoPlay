using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class ChatHub(string roomName) : Hub, IChat
{
    private readonly string _roomName = roomName;
    
    public async Task SendMessage(string message) => await Clients.Group(_roomName).SendAsync("ReceiveMessage", message);
    
    public async Task SendPrivateMessage(string connectionId, string message) => await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);

    public async Task UpdateMessage(string messageId, string message) => await Clients.Group(_roomName).SendAsync("UpdateMessage", messageId, message);
    
    public async Task DeleteMessage(string messageId) => await Clients.Group(_roomName).SendAsync("DeleteMessage", messageId);
}