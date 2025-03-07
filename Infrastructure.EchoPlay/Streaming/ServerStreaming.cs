using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Streaming.StreamingDtos;

namespace Infrastructure.EchoPlay.Streaming;

public class ServerStreaming:IStreaming<MediaFrameDto>
{
    public async Task ReceiveFrameAsync(MediaFrameDto data)
    {
        //упорядочить по тому от кого пришел фрейм 
        throw new NotImplementedException();
    }

    public async Task<MediaFrameDto> ReturnFrameAsync()
    {
        //отослать всем фреймы
        throw new NotImplementedException();
    }
}