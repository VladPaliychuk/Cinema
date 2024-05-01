using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.Username).IsRequired()
            .HasMaxLength(20);
        builder.Property(x => x.Password).IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.Role).IsRequired();
    }
}