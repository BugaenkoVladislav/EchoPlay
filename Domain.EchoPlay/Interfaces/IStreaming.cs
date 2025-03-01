namespace Domain.EchoPlay.Interfaces;

public interface IStreaming
{
    Task SendFrameAsync();  
    Task ReceiveFrameAsync();  
}