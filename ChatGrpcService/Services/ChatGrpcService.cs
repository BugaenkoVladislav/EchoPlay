using Grpc.Core;

namespace ChatGrpcService.Services;

public class ChatGrpcService:Chat.ChatBase
{
    public override Task<URL> CreateChat(Request request, ServerCallContext context)
    {
        return base.CreateChat(request, context);
    }

    public override Task<Result> DeleteChat(URL request, ServerCallContext context)
    {
        return base.DeleteChat(request, context);
    }

    public override Task<Result> DeleteMessage(MessageId request, ServerCallContext context)
    {
        return base.DeleteMessage(request, context);
    }

    public override Task<Message> GetMessage(MessageId request, ServerCallContext context)
    {
        return base.GetMessage(request, context);
    }

    public override Task<Result> SendMessage(Message request, ServerCallContext context)
    {
        return base.SendMessage(request, context);
    }

    public override Task<Result> UpdateMessage(MessageForUpdate request, ServerCallContext context)
    {
        return base.UpdateMessage(request, context);
    }
}