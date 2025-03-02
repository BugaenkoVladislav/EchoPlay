namespace Infrastructure.EchoPlay.Streaming.StreamingDtos;

public class VideoDataDto
{
    public byte[] Frame { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    // Преобразование из байтов в изображение и наоборот
    
}