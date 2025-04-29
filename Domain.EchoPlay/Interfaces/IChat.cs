namespace Domain.EchoPlay.Interfaces;

public interface IChat
{
    Task SendMessage(string roomName, string user,string message);
    Task UpdateMessage(string roomName, string user, string messageId,string message);
    Task DeleteMessage(string roomName, string user,string messageId);
}