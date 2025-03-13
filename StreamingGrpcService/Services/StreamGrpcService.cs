using App.EchoPlay.Services;
using Grpc.Core;

namespace StreamingGrpcService.Services;

public class StreamGrpcService:Streaming.StreamingBase
{
    //StreamingService _serverService = serverService;
    public override async Task StreamVideo(IAsyncStreamReader<MediaFrame> requestStream, IServerStreamWriter<MediaFrame> responseStream, ServerCallContext context)
    {
        //parse data
        //await _serverService.StreamServer();
    }
}