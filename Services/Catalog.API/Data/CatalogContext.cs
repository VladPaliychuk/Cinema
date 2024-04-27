using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ProductActor> ProductActors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ProductGenre> ProductGenres { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=CatalogDatabase;Username=VladPostgres;Password=VladPostgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product configuration
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Set primary key

            modelBuilder.Entity<Product>()
                .Property(p => p.Name) 
                .IsRequired() 
                .HasMaxLength(25); 
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Actor configuration
            modelBuilder.Entity<Actor>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Actor>()
                .Property(a => a.FirstName) 
                .IsRequired() 
                .HasMaxLength(100); 
            modelBuilder.Entity<Actor>()
                .Property(a => a.LastName) 
                .IsRequired() 
                .HasMaxLength(100); 

            // ProductActor configuration
            modelBuilder.Entity<ProductActor>()
                .HasKey(pa => new { pa.ProductId, pa.ActorId }); 

            modelBuilder.Entity<ProductActor>()
                .HasOne(pa => pa.Product) // Set relationship with Product
                .WithMany(p => p.ProductActors)
                .HasForeignKey(pa => pa.ProductId); // Set foreign key

            modelBuilder.Entity<ProductActor>()
                .HasOne(pa => pa.Actor) // Set relationship with Actor
                .WithMany(a => a.ProductActors)
                .HasForeignKey(pa => pa.ActorId); // Set foreign key
            
            // Genre configuration
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Id); // Set primary key

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired(); // Set constraints for Name

            // ProductGenre configuration
            modelBuilder.Entity<ProductGenre>()
                .HasKey(pg => new { pg.ProductId, pg.GenreId }); // Set composite primary key

            modelBuilder.Entity<ProductGenre>()
                .HasOne(pg => pg.Product) // Set relationship with Product
                .WithMany(p => p.ProductGenres)
                .HasForeignKey(pg => pg.ProductId); // Set foreign key

            modelBuilder.Entity<ProductGenre>()
                .HasOne(pg => pg.Genre) // Set relationship with Genre
                .WithMany(g => g.ProductsGenres)
                .HasForeignKey(pg => pg.GenreId); // Set foreign key
            
            // Screening configuration
            modelBuilder.Entity<Screening>()
                .HasKey(s => s.Id); // Set primary key

            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Product) // Set relationship with Product
                .WithMany(p => p.Screenings)
                .HasForeignKey(s => s.ProductId); // Set foreign key

            // Seat configuration
            modelBuilder.Entity<Seat>()
                .HasKey(s => s.Id); // Set primary key

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Screening) // Set relationship with Screening
                .WithMany(sc => sc.Seats)
                .HasForeignKey(s => s.ScreeningId); // Set foreign key
        }
    }
}
