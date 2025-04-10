using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class RoomHub(string roomName):Hub,IRoom
{
    private readonly string _roomName = roomName;
    
    public async Task JoinRoom()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, _roomName);
        await Clients.Group(_roomName).SendAsync("Notify", $"{Context.ConnectionId} вошёл в комнату {_roomName}.");
    }

    public async Task LeaveRoom()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _roomName);
        await Clients.Group(_roomName).SendAsync("Notify", $"{Context.ConnectionId} вошёл в комнату {_roomName}.");
    }
    
}