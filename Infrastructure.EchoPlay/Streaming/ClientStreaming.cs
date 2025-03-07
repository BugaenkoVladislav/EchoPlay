using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Streaming.StreamingDtos;

namespace Infrastructure.EchoPlay.Streaming;

public class ClientStreaming:IStreaming<MediaFrameDto>
{
    public async Task ReceiveFrameAsync(MediaFrameDto data)
    {
        // принимает все остальные фреймы
        throw new NotImplementedException();
    }

    public async Task<MediaFrameDto> ReturnFrameAsync()
    {
        // отправляет свой фрейм серверу
        throw new NotImplementedException();
    }
}