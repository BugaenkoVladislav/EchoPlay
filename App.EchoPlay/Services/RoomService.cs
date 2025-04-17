using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class RoomService(UnitOfWork uow, RoomCreator roomCreator)
{
    private readonly RoomCreator _roomCreator = roomCreator;
    private readonly UnitOfWork _uow = uow;
    private IRoom _room;
    private Dictionary<string, string> _users = new();
    
    public async Task JoinRoom(string roomName)
    {
        _roomCreator.URL = roomName;
        //todo пока затычка вынести в appSettings
        _room = _roomCreator.Create(RoomTypes.SignalR);
        await _room.JoinRoom();
        //_users.Add(,_userName);
    }

    public async Task LeaveRoom(string roomName)
    {
        await _room.LeaveRoom();
        //_users.Remove(,_userName);
    }

    public async Task GetUsersInRoom()
    {
        //_users
    }
}