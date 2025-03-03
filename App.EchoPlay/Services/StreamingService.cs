using Domain.EchoPlay.Interfaces;
using Grpc.Core;
using Infrastructure.EchoPlay.Streaming;
using Infrastructure.EchoPlay.Streaming.StreamingDtos;

namespace App.EchoPlay.Services;

public class StreamingService
{
    public StreamingService(ClientStreaming client, ServerStreaming server)
    {
        _client = client;
        _server = server;
    }

    private readonly IStreaming<MediaFrameDto> _client;
    private readonly IStreaming<MediaFrameDto> _server;

    public async Task StreamServerAsync(IAsyncStreamReader<MediaFrameDto> requestStream, IServerStreamWriter<MediaFrameDto> responseStream, ServerCallContext context)
    {
        try
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var chunk in requestStream.ReadAllAsync())
                {
                    // await _server.ReceiveFrameAsync();
                }
            });
            var writeTask = Task.Run(async () =>
            {
                await responseStream.WriteAsync(new MediaFrameDto()
                {
                    // await _server.SendFrameAsync();
                });
            });
            await Task.WhenAll(readTask, writeTask);
        }
        catch (Exception ex)
        {
        }
    }

    public async Task StreamClientAsync(AsyncDuplexStreamingCall<MediaFrameDto, MediaFrameDto> call)
    {
        try
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    // await _client.ReceiveFrameAsync();
                }
            });

            var writeTask = Task.Run(async () =>
            {
                await call.RequestStream.WriteAsync(new MediaFrameDto()
                {
                    // await _client.SendFrameAsync();
                });
            });
            await Task.WhenAll(readTask, writeTask);
        }
        catch (Exception ex)
        {
        }
    }
}