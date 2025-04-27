using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class ChatHub : Hub, IChat
{
    // Метод для отправки сообщения в комнату
    public async Task SendMessage(string roomName, string message)
    {
        // Отправляем сообщение всем пользователям в комнате
        await Clients.Group(roomName).SendAsync("ReceiveMessage", message);
    }

    // Метод для отправки личного сообщения
    public async Task SendPrivateMessage(string roomName,string connectionId, string message)
    {
        // Отправляем личное сообщение пользователю по его connectionId
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
    }

    // Метод для обновления сообщения в комнате
    public async Task UpdateMessage(string roomName, string messageId, string message)
    {
        // Отправляем обновленное сообщение всем пользователям в комнате
        await Clients.Group(roomName).SendAsync("UpdateMessage", messageId, message);
    }

    // Метод для удаления сообщения в комнате
    public async Task DeleteMessage(string roomName, string messageId)
    {
        // Отправляем команду на удаление сообщения всем пользователям в комнате
        await Clients.Group(roomName).SendAsync("DeleteMessage", messageId);
    }
}
