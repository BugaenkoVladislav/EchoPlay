using Domain.EchoPlay.Interfaces;
using Grpc.Net.Client;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class RoomService(UnitOfWork uow, IRoom room)
{
    private readonly IRoom _room = room;
    private readonly UnitOfWork _uow = uow;
    private Dictionary<string, string> _users = new();
    private readonly GrpcChannel _chatChannel;
    private readonly GrpcChannel _streamingChannel;
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