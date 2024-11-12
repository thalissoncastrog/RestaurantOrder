using Microsoft.EntityFrameworkCore;

namespace DrinkService.Models;

public class DbContextSettings : DbContext
{
    public DbContextSettings(DbContextOptions<DbContextSettings> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}