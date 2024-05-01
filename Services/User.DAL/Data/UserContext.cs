using Microsoft.EntityFrameworkCore;
using User.DAL.Configuration;

namespace User.DAL.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Entities.User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}