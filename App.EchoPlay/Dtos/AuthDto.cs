using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;

namespace App.EchoPlay.Dtos;

public class AuthDto
{
    public User UserData { get; set; }
    public AuthType? AuthType { get; set; } = null;
    public long? Code { get; set; } = null;
}