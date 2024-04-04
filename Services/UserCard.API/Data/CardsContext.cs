using Microsoft.EntityFrameworkCore;

namespace UserCard.API.Data;

public class CardsContext : DbContext
{
    public CardsContext(DbContextOptions<CardsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Entities.UserCard> UserCards { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}