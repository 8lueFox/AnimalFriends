using AF.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.Infrastructure.ModelConfigurations;

public static class EntityTypeConfigurationExtensions
{
    public static void ConfigDefaultProperties<T>(this EntityTypeBuilder<T> builder)
        where T : BaseAuditableEntity
    {
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        builder.Property(x => x.Created).IsRequired().HasColumnType("datetimeoffset")
            .HasDefaultValue(DateTimeOffset.Now);
        builder.Property(x => x.LastModified).HasColumnType("datetimeoffset");
    }
}