namespace Domain.EchoPlay.Interfaces;

public interface IChat
{
    Task SendMessage(string message);
    Task SendPrivateMessage(string connectionId,string message);
    Task UpdateMessage(string messageId,string message);
    Task DeleteMessage(string messageId);
}