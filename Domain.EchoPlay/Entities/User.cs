using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.EchoPlay.Entities;
[Index(nameof(Email), IsUnique = true)] 
[Index(nameof(Phone), IsUnique = true)] 
[Index(nameof(Username), IsUnique = true)]
public class User:TmpUser
{
    public string? Phone { get; set; } = null;
}