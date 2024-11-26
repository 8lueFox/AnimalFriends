using AF.Core.Database.Entities;
using AF.Core.Database.Enums;
using AF.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AF.Infrastructure.ModelConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigDefaultProperties();
        
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Phone).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.HashedPassword).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Gender).HasConversion(new EnumToStringConverter<Gender>())
            .HasMaxLength(20);
        builder.Property(x => x.Birthday).HasConversion<DateOnlyConverter>()
            .HasColumnType("date");
    }
}