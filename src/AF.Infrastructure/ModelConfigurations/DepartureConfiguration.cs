using AF.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.Infrastructure.ModelConfigurations;

public class DepartureConfiguration : IEntityTypeConfiguration<Departure>
{
    public void Configure(EntityTypeBuilder<Departure> builder)
    {
        builder.ConfigDefaultProperties();

        builder.Property(x => x.DepartureDate).HasDefaultValueSql("getdate()").HasColumnType("datetimeoffset");
        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasOne(b => b.User)
            .WithMany(a => a.Departures)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Departure_User");

        builder.HasOne(b => b.Animal)
            .WithMany(a => a.Departures)
            .HasForeignKey(b => b.AnimalId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Departure_Animal");
    }
}