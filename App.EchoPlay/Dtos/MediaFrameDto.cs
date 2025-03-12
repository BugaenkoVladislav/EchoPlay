namespace App.EchoPlay.Dtos;

public class MediaFrameDto
{
    public byte[] Data { get; set; } 
    public string Format { get; set; } = string.Empty;       // Формат контейнера
    public int Width { get; set; } = 0;                     
    public int Height { get; set; } = 0;                     
    public int Framerate { get; set; } = 0;                 
    public int Bitrate { get; set; } = 0;                  
    public string SenderId { get; set; } = string.Empty;  
}