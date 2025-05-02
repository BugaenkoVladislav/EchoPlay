using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

public class MediaHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _userConnections = new();

    public async Task JoinRoom(string roomId, string userId)
    {
        _userConnections[Context.ConnectionId] = userId;
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.OthersInGroup(roomId).SendAsync("NewUserConnected", userId);
    }

    public async Task SendSignal(string targetUserId, object signal)
    {
        var targetConnection = _userConnections.FirstOrDefault(x => x.Value == targetUserId).Key;
        if (targetConnection != null)
        {
            await Clients.Client(targetConnection).SendAsync("ReceiveSignal", 
                _userConnections[Context.ConnectionId], signal);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_userConnections.TryRemove(Context.ConnectionId, out var userId))
        {
            await Clients.Others.SendAsync("UserDisconnected", userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}