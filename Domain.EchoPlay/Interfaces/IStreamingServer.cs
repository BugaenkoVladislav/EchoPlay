using System.Threading.Channels;

namespace Domain.EchoPlay.Interfaces;

public interface IStreamingServer
{
    Task SendMessageForAllUsers(ChannelReader<byte[]> stream,string userId);
}