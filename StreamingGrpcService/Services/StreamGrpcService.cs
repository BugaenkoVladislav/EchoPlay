using Grpc.Core;

namespace StreamingGrpcService.Services;

public class StreamGrpcService:Streaming.StreamingBase
{
    public override async Task StreamVideo(IAsyncStreamReader<MediaFrame> requestStream, IServerStreamWriter<MediaFrame> responseStream, ServerCallContext context)
    {
        //Gets Data From Client
        
        //Send Client Other Data
    }
}