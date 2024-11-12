using Microsoft.EntityFrameworkCore;

namespace DesertService.Models;

public class DbContextSettings : DbContext
{
    public DbContextSettings(DbContextOptions<DbContextSettings> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}