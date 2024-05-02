namespace UserCard.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserCard.DAL.Entities;

public class UserCardConfiguration : IEntityTypeConfiguration<UserCard>
{
    public void Configure(EntityTypeBuilder<UserCard> builder)
    {
        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(uc => uc.Bonuses);

        builder.Property(uc => uc.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(uc => uc.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(uc => uc.EmailAddress)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(uc => uc.AddressLine)
            .HasMaxLength(200);

        builder.Property(uc => uc.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(uc => uc.State)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(uc => uc.ZipCode)
            .HasMaxLength(20);
    }
}