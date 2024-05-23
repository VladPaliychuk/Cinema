using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.HasKey(d => d.Id); 

        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
    }
}