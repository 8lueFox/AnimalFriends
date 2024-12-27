using AF.Core.Database.Entities;
using AF.Core.Database.Enums;
using AF.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AF.Infrastructure.ModelConfigurations;

internal class AdoptionConfiguration : IEntityTypeConfiguration<Adoption>
{
    public void Configure(EntityTypeBuilder<Adoption> builder)
    {
        builder.ConfigDefaultProperties();
        
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(1000);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Phone).HasMaxLength(20);
        
        builder.Property(x => x.AdoptionStatus).HasConversion(new EnumToStringConverter<AdoptionStatus>())
            .HasMaxLength(20);
        builder.Property(x => x.AdoptionDate).HasDefaultValueSql("getdate()").HasConversion<DateOnlyConverter>()
            .HasColumnType("date");
        
        builder.HasOne(b => b.Animal)
            .WithMany(a => a.Adoptions)
            .HasForeignKey(b => b.AnimalId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Adoption_Animal");
        builder.HasOne(b => b.Adopter)
            .WithMany(a => a.Adoptions)
            .HasForeignKey(b => b.AdopterId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Adoption_User");
    }
}