using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Streaming.StreamingDtos;

namespace Infrastructure.EchoPlay.Streaming;

public class ServerStreaming:IStreaming<MediaFrameDto>
{
    public async Task SendFrameAsync(MediaFrameDto data)
    {
        throw new NotImplementedException();
    }

    public async Task<MediaFrameDto> ReceiveFrameAsync()
    {
        throw new NotImplementedException();
    }
}