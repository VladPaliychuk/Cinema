using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }
        public DbSet<Coupon> Coupones { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=DiscountDatabase;Username=VladDiscount;Password=VladDiscount");
        }
    }
}
