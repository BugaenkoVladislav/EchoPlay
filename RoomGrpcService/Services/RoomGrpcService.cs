using App.EchoPlay.Services;
using Grpc.Core;
using Grpc.Net.Client;
using Infrastructure.EchoPlay.Hubs;

namespace RoomGrpcService.Services;

public class RoomGrpcService(RoomService roomService,GrpcChannel chatChannel,GrpcChannel streamingChannel) : Room.RoomBase
{
    private readonly ILogger<RoomGrpcService> _logger;
    private readonly RoomService _roomService = roomService;
    private readonly GrpcChannel _chatChannel = chatChannel;
    private readonly GrpcChannel _streamingChannel = streamingChannel;
    public override async Task<Result> JoinRoom(URL request, ServerCallContext context)
    {
        try
        {
            await _roomService.JoinRoom();
            var chatClient= new Chat.ChatClient(_chatChannel);
            //var streamingClient = new 
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                Code = 400,
                Desc = ex.Message,
            });
        }

        return await Task.FromResult(new Result()
        {
            Code = 200,
            Desc = "Success",
        });
    }

    public override async Task<Result> LeaveRoom(URL request, ServerCallContext context)
    {
        try
        {
            await _roomService.LeaveRoom();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                Code = 400,
                Desc = ex.Message,
            });
        }

        return await Task.FromResult(new Result()
        {
            Code = 200,
            Desc = "Success",
        });
    }
}