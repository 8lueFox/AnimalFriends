using AF.Core.Database.Entities;
using AF.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.Infrastructure.ModelConfigurations;

public class ShelterUserConfigurationExtensions : IEntityTypeConfiguration<ShelterUser>
{
    public void Configure(EntityTypeBuilder<ShelterUser> builder)
    {
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasKey(e => new { e.UserId, e.ShelterId });

        builder.Property(x => x.StarDate)
            .IsRequired()
            .HasDefaultValueSql("getdate()")
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");
        builder.Property(x => x.IsOwner).HasDefaultValue(false);
        builder.Property(x => x.IsAdmin).HasDefaultValue(false);

        builder.HasOne(b => b.User)
            .WithMany(a => a.Shelters)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ShelterUser_User");
        builder.HasOne(b => b.Shelter)
            .WithMany(a => a.Users)
            .HasForeignKey(b => b.ShelterId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ShelterUser_Shelter");
    }
}