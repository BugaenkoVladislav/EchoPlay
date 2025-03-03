using App.EchoPlay.Services;
using Grpc.Core;
using Infrastructure.EchoPlay.Streaming;

namespace StreamingGrpcService.Services;

public class StreamGrpcService(StreamingService serverService):Streaming.StreamingBase
{
    StreamingService _serverService = serverService;
    public override async Task StreamVideo(IAsyncStreamReader<MediaFrame> requestStream, IServerStreamWriter<MediaFrame> responseStream, ServerCallContext context)
    {
        //parse data
        //await _serverService.StreamServer();
    }
}