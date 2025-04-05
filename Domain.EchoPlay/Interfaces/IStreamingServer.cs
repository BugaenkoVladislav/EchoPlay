namespace Domain.EchoPlay.Interfaces;

public interface IStreamingServer
{
    Task SendMessageForAllUsers(string userId,byte[] data);
}