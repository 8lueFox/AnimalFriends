using AF.Core.Database.Entities;
using AF.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.Infrastructure.ModelConfigurations;

public class VetVisitConfiguration : IEntityTypeConfiguration<VetVisit>
{
    public void Configure(EntityTypeBuilder<VetVisit> builder)
    {
        builder.Property(x => x.VetName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Diagnosis).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Treatment).IsRequired().HasMaxLength(2000);
        
        builder.Property(x => x.VisitDate).HasDefaultValueSql("getdate()").HasConversion<DateOnlyConverter>()
            .HasColumnType("date");
        
        builder.HasOne(b => b.Animal)
            .WithMany(a => a.VetVisits)
            .HasForeignKey(b => b.AnimalId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Animal_VetVisit");
    }
}