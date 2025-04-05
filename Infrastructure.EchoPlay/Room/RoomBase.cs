using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Room;

public class RoomBase(IStreamingServer streamingServer, IChat chat):IRoom
{
    public IStreamingServer StreamingServer { get; set; } = streamingServer;
    public IChat Chat { get; set; } = chat;
    public async Task CreateRoom()
    {
        var roomId = Guid.NewGuid();
        
    }

    public async Task JoinRoom(Guid roomId)
    {
        throw new NotImplementedException();
    }

    public async Task LeaveRoom()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRoom()
    {
        throw new NotImplementedException();
    }
}