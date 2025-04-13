using Domain.EchoPlay.Interfaces;
using Grpc.Net.Client;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class RoomService
{
    private readonly IRoom _room;
    private readonly UnitOfWork _uow;
    private Dictionary<string, string> _users = new();
    public RoomService(UnitOfWork uow, IRoom room, IStreamingServer server, IChat chat)
    {
        _room = room;
        _uow = uow;
        using var channel = GrpcChannel.ForAddress("https://localhost:7042");
        //var client = new Greeter.GreeterClient(channel);
    }
    
    

    public async Task JoinRoom()
    {
        await _room.JoinRoom();
        
        //_users.Add(,_userName);
    }

    public async Task LeaveRoom()
    {
        await _room.LeaveRoom();
        //_users.Remove(,_userName);
    }

    public async Task GetUsersInRoom()
    {
        //_users
    }
}