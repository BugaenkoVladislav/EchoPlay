using System.Collections.Concurrent;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.SignalR;

public class WebRtcHub : Hub
{
    
    public async Task SendSignal(string groupName, string senderId, string signalType, object data)
    {
        await Clients.Group(groupName).SendAsync("ReceiveSignal", senderId, signalType, data);
    }

    public async Task GetOnlineUsers(string groupName)
    {
        await Clients.Caller.SendAsync("OnlineUsers",WebRtcConnectionManager.GetUsersFromGroup(groupName));
    }

    public async Task JoinGroup(string groupName)
    {
        var userId = Context.ConnectionId;
        WebRtcConnectionManager.AddUserInGroup(groupName,userId);
        await Groups.AddToGroupAsync(userId, groupName);
        await Clients.Group(groupName).SendAsync("Notify", $"{userId} joined the group {groupName}.");
    }

    public async Task LeaveGroup(string groupName)
    {
        var userId = Context.ConnectionId;
        WebRtcConnectionManager.RemoveUserFromGroup(groupName,userId);
        await Groups.RemoveFromGroupAsync(userId, groupName);
        await Clients.Group(groupName).SendAsync("Notify", $"{userId} left the group {groupName}.");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}

internal static class WebRtcConnectionManager
{
    private static ConcurrentDictionary<string, string> _users = new();

    public static void AddUserInGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryAdd(compositeKey, userId);
    }

    public static void RemoveUserFromGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryRemove(compositeKey, out _);
    }

    public static List<string> GetUsersFromGroup(string groupName)
    {
        return (from kvp in _users where kvp.Key.StartsWith($"{groupName}_") select kvp.Key.Substring(groupName.Length + 1)).ToList();
    }

    public static bool IsUserInGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        return _users.ContainsKey(compositeKey);
    }
}