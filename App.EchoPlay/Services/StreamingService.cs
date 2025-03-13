using App.EchoPlay.Dtos;
using Domain.EchoPlay.Interfaces;
using Grpc.Core;

namespace App.EchoPlay.Services;

// public class StreamingService(Queue<MediaFrameDto>framesQueue)
// {
//     private readonly Queue<MediaFrameDto> _framesQueue = framesQueue;
//     public async Task StreamServerAsync(IAsyncStreamReader<MediaFrameDto> requestStream, IServerStreamWriter<MediaFrameDto> responseStream, ServerCallContext context)
//     {
//         try
//         {
//             var readTask = Task.Run(async () =>
//             {
//                 await foreach (var chunk in requestStream.ReadAllAsync())
//                 {
//                     _framesQueue.Enqueue(chunk);
//                 }
//             });
//             var writeTask = Task.Run(async () =>
//             {
//                 while (!context.CancellationToken.IsCancellationRequested)
//                 {
//                     var frame = _framesQueue.Dequeue();
//                     await responseStream.WriteAsync(frame);
//                 }
//             });
//             await Task.WhenAll(readTask, writeTask);
//         }
//         catch (Exception ex)
//         {
//         }
//     }
//     
//     public async Task StreamClientAsync(AsyncDuplexStreamingCall<MediaFrameDto, MediaFrameDto> call)
//     {
//         try
//         {
//             var readTask = Task.Run(async () =>
//             {
//                 await foreach (var chunk in call.ResponseStream.ReadAllAsync())
//                 {
//                     //отправляем js этот фрейм
//                 }
//             });
//     
//             var writeTask = Task.Run(async () =>
//             {
//                 while (true) 
//                 {
//                     // получаем с js этот фрейм
//                 }
//             });
//             await Task.WhenAll(readTask, writeTask);
//         }
//         catch (Exception ex)
//         {
//         }
//     }
// }