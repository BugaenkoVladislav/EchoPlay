using System.ComponentModel.DataAnnotations;

namespace App.EchoPlay.Dtos;

public class LoginPasswordDto
{
    public string? Email { get; set; } 

    public string? Password { get; set; }
}