using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;

namespace App.EchoPlay.Dtos;

public class AuthDto
{
    public LoginPasswordDto? UserData { get; set; } = null;
    public AuthType? AuthType { get; set; } = null;
    public long? Code { get; set; } = null;
}