using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductGenreConfiguration : IEntityTypeConfiguration<ProductGenre>
{
    public void Configure(EntityTypeBuilder<ProductGenre> builder)
    {
        builder.HasKey(pg => new { pg.ProductId, pg.GenreId }); 

        builder
            .HasOne(pg => pg.Product) 
            .WithMany(p => p.ProductGenres)
            .HasForeignKey(pg => pg.ProductId); 
        
        builder
            .HasOne(pg => pg.Genre) 
            .WithMany(g => g.ProductsGenres)
            .HasForeignKey(pg => pg.GenreId); 
    }
}