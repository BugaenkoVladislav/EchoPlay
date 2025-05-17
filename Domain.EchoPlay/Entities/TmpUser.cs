using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.EchoPlay.Entities;
[Index(nameof(Email), IsUnique = true)] 
[Index(nameof(Username), IsUnique = true)]
public class TmpUser
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    
    public string Username { get; set; } = null;
    
    [Required]
    public string Password { get; set; }= null!;
    
    [Required]
    public string Email { get; set; }= null!;
}