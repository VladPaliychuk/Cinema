using Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=CatalogDatabase;Username=VladPostgres;Password=VladPostgres");
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //
        //     modelBuilder.Entity<Product>().HasData(
        //         new Product() { Id = Guid.NewGuid(), Name = "Very Scary Movie", Category = "Horror", Price = 120 },
        //         new Product() { Id = Guid.NewGuid(), Name = "Very Funny Movie", Category = "Comedy", Price = 120 },
        //         new Product() { Id = Guid.NewGuid(), Name = "Very Enigmatic Movie", Category = "Thriller", Price = 150 });
        // }
    }
}
