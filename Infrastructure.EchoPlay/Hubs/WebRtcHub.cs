using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class WebRtcHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _users = new();

    public async Task SendSignal(string groupName, string senderId, string signalType, object data)
    {
        await Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("ReceiveSignal", senderId, signalType, data);
    }

    public async Task JoinGroup(string groupName, string username)
    {
        var userId = Context.ConnectionId;
        WebRtcConnectionManager.AddUserToGroup(groupName, userId);
        await Groups.AddToGroupAsync(userId, groupName);
        WebRtcConnectionManager.AddUserNameId(userId,username);
        var existingUsers = WebRtcConnectionManager.GetUsersInGroup(groupName).Where(u => u != userId).ToList();
        await Clients.Caller.SendAsync("ExistingUsers", existingUsers);
        await Clients.GroupExcept(groupName, userId).SendAsync("UserJoined", userId);
        await Clients.Group(groupName).SendAsync("Notify", $"{userId} присоединился к группе {groupName}.");
    }

    public async Task LeaveGroup(string groupName)
    {
        var userId = Context.ConnectionId;
        WebRtcConnectionManager.RemoveUserFromGroup(groupName, userId);
        WebRtcConnectionManager.RemoveUserNameId(userId);
        await Groups.RemoveFromGroupAsync(userId, groupName);

        await Clients.Group(groupName).SendAsync("UserLeft", userId);
        await Clients.Group(groupName).SendAsync("Notify", $"{userId} покинул группу {groupName}.");
    }

    public string GetUsername(string userId) => WebRtcConnectionManager.GetUserNameFromId(userId);

    public async Task SendMicrophoneState(string roomId, string userId, bool state)
    {
        await Clients.Group(roomId).SendAsync("ReceiveMicrophoneState", userId, state);
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.ConnectionId;
        var userGroups = _users.Where(u => u.Value == userId).Select(u => u.Key.Split('_')[0]).Distinct();

        foreach (var group in userGroups)
        {
            await LeaveGroup(group);
        }

        await base.OnDisconnectedAsync(exception);
    }
}

internal static class WebRtcConnectionManager
{
    private static ConcurrentDictionary<string, string> _users = new();
    private static ConcurrentDictionary<string, string> _usersIdNames = new();

    public static void AddUserToGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryAdd(compositeKey, userId);
    }

    public static void RemoveUserFromGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryRemove(compositeKey, out _);
    }

    public static List<string> GetUsersInGroup(string groupName)
    {
        return _users.Where(u => u.Key.StartsWith($"{groupName}_"))
            .Select(u => u.Value)
            .ToList();
    }

    public static string GetUserNameFromId(string userId)
    {
        return _usersIdNames[userId];
    }
    
    public static void AddUserNameId(string userId,string username)
    {
        _usersIdNames.AddOrUpdate(userId, username, (k, v) => username);
    }
    public static void RemoveUserNameId(string userId)
    {
        var note = GetUserNameFromId(userId);
        _usersIdNames.TryRemove(userId,out _);
    }
}