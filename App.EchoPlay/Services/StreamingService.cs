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
                    await _server.ReceiveFrameAsync(chunk);
                }
            });
            var writeTask = Task.Run(async () =>
            {
                while (!context.CancellationToken.IsCancellationRequested)
                {
                    var frame = await _server.ReturnFrameAsync();
                    await responseStream.WriteAsync(frame);
                }
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
                await foreach (var chunk in call.ResponseStream.ReadAllAsync())
                {
                    await _client.ReceiveFrameAsync(chunk);
                }
            });

            var writeTask = Task.Run(async () =>
            {
                while (true) 
                {
                    var frame = await _client.ReturnFrameAsync();
                    await call.RequestStream.WriteAsync(frame);
                }
            });
            await Task.WhenAll(readTask, writeTask);
        }
        catch (Exception ex)
        {
        }
    }
}