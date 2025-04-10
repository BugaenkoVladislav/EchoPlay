using App.EchoPlay.Services;
using Grpc.Core;
using Infrastructure.EchoPlay.Hubs;

namespace RoomGrpcService.Services;

public class RoomGrpcService(RoomService roomService):Room.RoomBase
{
    private readonly ILogger<RoomGrpcService> _logger;
    private RoomService _roomService = roomService;

    public override async Task<Result> JoinRoom(RoomURL request, ServerCallContext context)
    {
        try
        { 
            await _roomService.JoinRoom();
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

    public override async Task<Result> LeaveRoom(RoomURL request, ServerCallContext context)
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