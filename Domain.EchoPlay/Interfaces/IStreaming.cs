namespace Domain.EchoPlay.Interfaces;

public interface IStreaming<TData>
{
    Task SendFrameAsync(TData data);  
    Task<TData> ReceiveFrameAsync();  
}