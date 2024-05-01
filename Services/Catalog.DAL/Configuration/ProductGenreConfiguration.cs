using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductGenreConfiguration : IEntityTypeConfiguration<ProductGenre>
{
    public void Configure(EntityTypeBuilder<ProductGenre> builder)
    {
        builder.HasKey(pg => new { pg.ProductId, pg.GenreId }); // Set composite primary key

        builder
            .HasOne(pg => pg.Product) // Set relationship with Product
            .WithMany(p => p.ProductGenres)
            .HasForeignKey(pg => pg.ProductId); // Set foreign key
        
        builder
            .HasOne(pg => pg.Genre) // Set relationship with Genre
            .WithMany(g => g.ProductsGenres)
            .HasForeignKey(pg => pg.GenreId); // Set foreign key
    }
}