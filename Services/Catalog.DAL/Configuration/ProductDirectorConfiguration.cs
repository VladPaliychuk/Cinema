using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductDirectorConfiguration : IEntityTypeConfiguration<ProductDirector>
{
    public void Configure(EntityTypeBuilder<ProductDirector> builder)
    {
        builder.HasKey(pd => new { pd.ProductId, pd.DirectorId }); 

        builder
            .HasOne(pd => pd.Product) 
            .WithMany(p => p.ProductDirectors)
            .HasForeignKey(pd => pd.ProductId); 

        builder
            .HasOne(pd => pd.Director)
            .WithMany(d => d.ProductDirectors)
            .HasForeignKey(pd => pd.DirectorId); 
    }
}