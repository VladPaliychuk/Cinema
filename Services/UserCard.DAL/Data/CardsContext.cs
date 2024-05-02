using Microsoft.EntityFrameworkCore;
using UserCard.DAL.Configuration;

namespace UserCard.DAL.Data;

public class CardsContext : DbContext
{
    public CardsContext(DbContextOptions<CardsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Entities.UserCard> UserCards { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.ApplyConfiguration(new UserCardConfiguration());
    }
}