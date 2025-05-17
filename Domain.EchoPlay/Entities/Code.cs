using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.EchoPlay.Entities;
public class Code
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string Number { get; set; }
    
    [ForeignKey("UserId")]
    public TmpUser User { get; set; }
}