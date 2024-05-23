using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Row)
            .IsRequired();

        builder.Property(s => s.Number)
            .IsRequired(); 
        
        builder
            .HasOne(s => s.Screening) 
            .WithMany(sc => sc.Seats)
            .HasForeignKey(s => s.ScreeningId); 
    }
}