namespace Infrastructure.EchoPlay.Streaming.StreamingDtos;

public class AudioDataDto
{
    public byte[] Samples { get; set; }
    public int SampleRate { get; set; }
    public int Channels { get; set; }

    // Преобразование из байтов в звук и наоборот
    
}