using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Room;

public class RoomBase:IRoom
{
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

    public async Task SendMessage(string message)
    {
        throw new NotImplementedException();
    }
}