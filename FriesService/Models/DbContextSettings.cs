using Microsoft.EntityFrameworkCore;
using OrderLibrary.Models;

namespace FriesService.Models;

public class DbContextSettings : DbContext
{
    public DbContextSettings(DbContextOptions<DbContextSettings> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}