using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Streaming;
using Infrastructure.EchoPlay.Streaming.StreamingDtos;

namespace App.EchoPlay.Services;

public class StreamingService(ClientStreaming client, ServerStreaming server)
{
    private readonly IStreaming<MediaFrameDto> _client = client;
    private readonly IStreaming<MediaFrameDto> _server = server;
    public async Task StreamServer()
    {
        // await _server.ReceiveFrameAsync();
        // await _server.SendFrameAsync();
    }

    public async Task StreamClient()
    {
        // await _client.SendFrameAsync();
        // await _client.ReceiveFrameAsync();
    }
}