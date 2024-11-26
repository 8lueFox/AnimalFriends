using AF.Core.Database.Entities;
using AF.Core.Database.Enums;
using AF.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AF.Infrastructure.ModelConfigurations;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ConfigDefaultProperties();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Gender).HasConversion(new EnumToStringConverter<Gender>())
            .HasMaxLength(20);
        builder.Property(x => x.Species).HasMaxLength(100);
        builder.Property(x => x.Breed).HasMaxLength(100);
        builder.Property(x => x.Age).HasMaxLength(100);
        builder.Property(x => x.HealthStatus).HasMaxLength(100);
        builder.Property(x => x.ArrivalDate).HasDefaultValue(DateTime.Now).HasConversion<DateOnlyConverter>()
            .HasColumnType("date");

        builder.HasOne(b => b.Shelter)
            .WithMany(a => a.Animals)
            .HasForeignKey(b => b.ShelterId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Animal_Shelter");

        builder.HasOne(b => b.AssignedUser)
            .WithMany(a => a.AssignedAnimals)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Animal_User");
    }
}