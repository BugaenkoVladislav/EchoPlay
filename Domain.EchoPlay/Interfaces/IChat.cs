namespace Domain.EchoPlay.Interfaces;

public interface IChat
{
    Task SendMessage(string message);
    Task UpdateMessage(Guid messageId,string message);
    Task DeleteMessage(Guid messageId);
}