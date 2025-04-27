using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class RoomHub : Hub
{
    // Метод для присоединения пользователя к комнате
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("Notify", $"{Context.ConnectionId} вошёл в комнату {roomName}.");
    }

    // Метод для выхода из комнаты
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("Notify", $"{Context.ConnectionId} вышел из комнаты {roomName}.");
    }
}