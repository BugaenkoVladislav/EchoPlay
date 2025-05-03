using System.Collections.Concurrent;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.SignalR;

public class WebRtcHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _users = new();

    public async Task SendSignal(string groupName, string senderId, string signalType, object data)
    {
        await Clients.GroupExcept(groupName, Context.ConnectionId)
            .SendAsync("ReceiveSignal", senderId, signalType, data);
    }

    public async Task JoinGroup(string groupName)
    {
        var userId = Context.ConnectionId;
        AddUserToGroup(groupName, userId);
        await Groups.AddToGroupAsync(userId, groupName);

        var existingUsers = GetUsersInGroup(groupName)
            .Where(u => u != userId)
            .ToList();
        await Clients.Caller.SendAsync("ExistingUsers", existingUsers);
        
        await Clients.GroupExcept(groupName, userId)
            .SendAsync("UserJoined", userId);

        await Clients.Group(groupName).SendAsync("Notify", $"{userId} присоединился к группе {groupName}.");
    }

    public async Task LeaveGroup(string groupName)
    {
        var userId = Context.ConnectionId;
        RemoveUserFromGroup(groupName, userId);
        await Groups.RemoveFromGroupAsync(userId, groupName);

        await Clients.Group(groupName).SendAsync("UserLeft", userId);
        await Clients.Group(groupName).SendAsync("Notify", $"{userId} покинул группу {groupName}.");
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

    private void AddUserToGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryAdd(compositeKey, userId);
    }

    private void RemoveUserFromGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        _users.TryRemove(compositeKey, out _);
    }

    private List<string> GetUsersInGroup(string groupName)
    {
        return _users.Where(u => u.Key.StartsWith($"{groupName}_"))
            .Select(u => u.Value)
            .ToList();
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
        return (from kvp in _users
            where kvp.Key.StartsWith($"{groupName}_")
            select kvp.Key.Substring(groupName.Length + 1)).ToList();
    }

    public static bool IsUserInGroup(string groupName, string userId)
    {
        var compositeKey = $"{groupName}_{userId}";
        return _users.ContainsKey(compositeKey);
    }
}