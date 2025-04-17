using Domain.EchoPlay.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EchoPlay;

public class MyDbContext(DbContextOptions<MyDbContext> options):DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Code> Codes { get; set; }
}