using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.EchoPlay.Entities;
public record Code
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string Number { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
}