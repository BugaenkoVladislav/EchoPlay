namespace Domain.EchoPlay.Interfaces;

public interface IStreamingClient
{
    Task SendMessageAsync(byte[] data);
    
    Task StartStreamingAsync();
    Task StopStreamingAsync();
    
}