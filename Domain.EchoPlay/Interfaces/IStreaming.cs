namespace Domain.EchoPlay.Interfaces;

public interface IStreaming<TData>
{
    Task ReceiveFrameAsync(TData data);  
    Task<TData> ReturnFrameAsync();  
}