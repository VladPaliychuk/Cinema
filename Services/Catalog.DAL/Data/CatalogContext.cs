using Catalog.DAL.Configuration;
using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Data
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
        public DbSet<Director> Directors { get; set; }
        public DbSet<ProductDirector> ProductDirectors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ActorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductActorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ProductGenreConfiguration());
            modelBuilder.ApplyConfiguration(new ScreeningConfiguration());
            modelBuilder.ApplyConfiguration(new SeatConfiguration());
            modelBuilder.ApplyConfiguration(new DirectorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDirectorConfiguration());
        }
    }
}
