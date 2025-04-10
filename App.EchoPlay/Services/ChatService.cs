using Domain.EchoPlay.Entities;
using Infrastructure.EchoPlay;
using Infrastructure.EchoPlay.Hubs;

namespace App.EchoPlay.Services;

public class ChatService(ChatHub chatHub,UnitOfWork uow)
{
    private readonly ChatHub _chatHub = chatHub;
    private readonly UnitOfWork _uow = uow;

    public async Task SendMessage(Message message)
    {
        
    }

    public async Task SendPrivateMessage(Message message)
    {
        
    }

    public async Task DeleteMessage(Message message)
    {
        
    }

    public async Task UpdateMessage(Message message)
    {
        
    }
}