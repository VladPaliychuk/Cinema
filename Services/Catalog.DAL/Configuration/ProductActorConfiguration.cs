using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ProductActorConfiguration : IEntityTypeConfiguration<ProductActor>
{
    public void Configure(EntityTypeBuilder<ProductActor> builder)
    {
        builder.HasKey(pa => new { pa.ProductId, pa.ActorId }); 
        
        builder
            .HasOne(pa => pa.Product) 
            .WithMany(p => p.ProductActors)
            .HasForeignKey(pa => pa.ProductId); 
        
        builder
            .HasOne(pa => pa.Actor) 
            .WithMany(a => a.ProductActors)
            .HasForeignKey(pa => pa.ActorId); 
    }
}