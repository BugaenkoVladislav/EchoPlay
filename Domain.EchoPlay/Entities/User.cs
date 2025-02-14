using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.EchoPlay.Entities;
[Index(nameof(Email), IsUnique = true)] 
[Index(nameof(Phone), IsUnique = true)] 
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Username { get; set; }= null!;
    
    [Required]
    public string Password { get; set; }= null!;
    
    [Required]
    public string Email { get; set; }= null!;
    
    [Required]
    public bool Is2FA { get; set; }= false;
    
    public string Phone { get; set; } = null!;
}