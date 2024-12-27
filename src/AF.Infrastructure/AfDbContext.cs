using System.Reflection;
using AF.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AF.Infrastructure;

public class AfDbContext : DbContext
{
    public AfDbContext() {}

    public AfDbContext(DbContextOptions<AfDbContext> opts)
        : base(opts)
    {
    }
    
    internal virtual DbSet<Adoption> Adoptions { get; set; }
    internal virtual DbSet<Animal> Animals { get; set; }
    internal virtual DbSet<Departure> Departures { get; set; }
    internal virtual DbSet<Shelter> Shelters { get; set; }
    internal virtual DbSet<ShelterUser> ShelterUsers { get; set; }
    internal virtual DbSet<User> Users { get; set; }
    internal virtual DbSet<VetVisit> VetVisits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}