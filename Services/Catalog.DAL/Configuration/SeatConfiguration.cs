using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(s => s.Id); // Set primary key

        builder.Property(s => s.Row)
            .IsRequired(); // Set constraints for Row

        builder.Property(s => s.Number)
            .IsRequired(); // Set constraints for Number
        
        builder
            .HasOne(s => s.Screening) // Set relationship with Screening
            .WithMany(sc => sc.Seats)
            .HasForeignKey(s => s.ScreeningId); // Set foreign key
    }
}