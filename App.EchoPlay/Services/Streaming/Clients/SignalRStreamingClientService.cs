using App.EchoPlay.Dtos;
using Domain.EchoPlay.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace App.EchoPlay.Services.Streaming.Clients;

public class SignalRStreamingClientService:IStreamingClient
{
    //Коллкция фреймов
    //private Queue<MediaFrame>
    
    private readonly HubConnection _connection;
    private readonly string _userId;
    public SignalRStreamingClientService(string hubUrl)
    {
        //todo add url
        _connection = new HubConnectionBuilder().Build(); ;
    }
    
    
    public async Task SendMessageAsync(byte[] data)
    {
        var user = new Guid();
        await _connection.SendAsync("SendMessageForAllUsers", user, data);
    }

    public async Task StartStreamingAsync()
    {
        _connection.On<Guid,byte[]>("ReceiveFromServerForAllUsers", (user, message) =>
        {
            Console.WriteLine($"{user} : {message}");
        });
        
        await _connection.StartAsync();
        
        //Добавить логику
        var bytes = new byte[1024];
        _ = Task.Run(async () =>
        {
            while (true)
            {
                await SendMessageAsync(bytes);
            }
        });

    }

    public async Task StopStreamingAsync()
    {
        await _connection.StopAsync();
    }
}