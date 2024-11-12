using Microsoft.EntityFrameworkCore;

namespace GrillService.Models;

public class DbContextSettings : DbContext
{
    public DbContextSettings(DbContextOptions<DbContextSettings> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}