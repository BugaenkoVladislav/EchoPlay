﻿using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class ChatHub : Hub, IChat
{
    public async Task SendMessage(string roomId, string user, string message)
    {
        var messageId = Guid.NewGuid().ToString();
        await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message, messageId);
    }

    public async Task UpdateMessage(string roomName, string user, string messageId, string message)
    {
        await Clients.Group(roomName).SendAsync("UpdateMessage", messageId, message, user);
    }

    public async Task DeleteMessage(string roomName, string user, string messageId)
    {
        await Clients.Group(roomName).SendAsync("DeleteMessage", messageId, user);
    }
    public async Task JoinRoom(string username,string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("Notify", $"{username} вошёл в комнату {roomName}.");
    }
    
    public async Task LeaveRoom(string username,string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("Notify", $"{username} вышел из комнаты {roomName}.");
    }
}
