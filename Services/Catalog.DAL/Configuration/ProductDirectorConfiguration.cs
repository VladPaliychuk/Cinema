using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductDirectorConfiguration : IEntityTypeConfiguration<ProductDirector>
{
    public void Configure(EntityTypeBuilder<ProductDirector> builder)
    {
        builder.HasKey(pd => new { pd.ProductId, pd.DirectorId }); // Set primary key

        builder
            .HasOne(pd => pd.Product) // Set relationship with Product
            .WithMany(p => p.ProductDirectors)
            .HasForeignKey(pd => pd.ProductId); // Set foreign key

        builder
            .HasOne(pd => pd.Director) // Set relationship with Director
            .WithMany(d => d.ProductDirectors)
            .HasForeignKey(pd => pd.DirectorId); // Set foreign key
    }
}