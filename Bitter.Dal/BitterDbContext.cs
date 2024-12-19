using Bitter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitter.Dal;

public class BitterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=bitterdb;User=server@localhost;Password=ServerHaslo7777!!",
                new MySqlServerVersion(new Version(8, 0, 40)));
        }
    }
}