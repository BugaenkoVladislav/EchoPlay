namespace Domain.EchoPlay.Interfaces;

public interface IRoom
{
    Task JoinRoom(string roomName);
    Task LeaveRoom(string roomName);
}