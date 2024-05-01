using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); // Set primary key

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(25);
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}