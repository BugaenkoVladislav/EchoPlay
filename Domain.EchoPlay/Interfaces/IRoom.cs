namespace Domain.EchoPlay.Interfaces;

public interface IRoom
{
    Task CreateRoom();
    Task JoinRoom(Guid roomId);
    Task LeaveRoom();
    Task DeleteRoom();
    Task SendMessage(string message);
}