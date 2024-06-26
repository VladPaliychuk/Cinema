﻿using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DAL.Configuration;

public class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
{
    public void Configure(EntityTypeBuilder<Screening> builder)
    {
        builder.HasKey(s => s.Id); 

        builder
            .HasOne(s => s.Product) 
            .WithMany(p => p.Screenings)
            .HasForeignKey(s => s.ProductId);
    }
}