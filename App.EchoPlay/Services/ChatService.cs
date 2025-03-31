using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;
using Infrastructure.EchoPlay.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace App.EchoPlay.Services;

public class ChatService(ChatHub chatHub,UnitOfWork uow)
{
    private readonly ChatHub _chatHub = chatHub;
    private readonly UnitOfWork _uow = uow;
    //отправка сообщений изменение и их удаление
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