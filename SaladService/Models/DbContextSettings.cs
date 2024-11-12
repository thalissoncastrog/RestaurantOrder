using Microsoft.EntityFrameworkCore;

namespace SaladService.Models;

public class DbContextSettings : DbContext
{
    public DbContextSettings(DbContextOptions<DbContextSettings> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}