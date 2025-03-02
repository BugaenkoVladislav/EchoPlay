namespace Infrastructure.EchoPlay.Streaming.StreamingDtos;

public class MediaFrameDto
{
    public VideoDataDto Video { get; set; }
    public AudioDataDto Audio { get; set; }
    public Guid SenderId { get; set; }
    public long Timestamp { get; set; }
}