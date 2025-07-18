using ConcurrencyChallenger.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyChallenger.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
              .Property(p => p.RowVersion)
              .IsRowVersion();

        modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Laptop", Stock = 3 },
        new Product { Id = 2, Name = "Keyboard", Stock = 10 },
        new Product { Id = 3, Name = "Monitor", Stock = 7 });
    }
}