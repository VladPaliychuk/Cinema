using Microsoft.EntityFrameworkCore;

namespace User.API.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Entities.User> Users { get; set; }
}