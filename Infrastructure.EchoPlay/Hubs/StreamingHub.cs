using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.EchoPlay.Hubs;

public class StreamingHub : Hub
{
    // Метод для отправки видеофрейма всем пользователям в комнате
    public async Task SendFrameForAllUsers(string roomName, string userId, byte[] frameData)
    {
        // Отправляем данные видео всем пользователям в комнате, кроме того, кто прислал
        await Clients.Group(roomName).SendAsync("ReceiveFrameForAllUsers", userId, frameData);
    }
}
