using System.Threading.Channels;
using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;

namespace App.EchoPlay.Services;

public class StreamingService(StreamingCreator creator,UnitOfWork uow)
{
    private readonly StreamingCreator _streamingCreator = creator;
    private IStreamingServer _streamingServer;
    private readonly UnitOfWork _uow = uow;
    
    public async Task StartStreaming(ChannelReader<byte[]> stream,StreamingType streamingType)
    {
        _streamingServer = _streamingCreator.Create(streamingType);
        //todo указать юзер нейм реальный
        var userName = "";
        _ = _streamingServer.SendMessageForAllUsers(stream, userName);
    }
}