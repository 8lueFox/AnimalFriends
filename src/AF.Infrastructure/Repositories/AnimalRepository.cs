using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class AnimalRepository(AfDbContext dbContext) : IAnimalRepository
{
    public IQueryable<Animal> Items { get; } = dbContext.Animals;
    
    public Animal? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(Animal obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.ShelterId = obj.ShelterId;
        entity.UserId = obj.UserId;
        entity.Name = obj.Name;
        entity.Gender = obj.Gender;
        entity.Species = obj.Species;
        entity.Breed = obj.Breed;
        entity.ArrivalDate = obj.ArrivalDate;
        entity.Age = obj.Age;
        entity.HealthStatus = obj.HealthStatus;
        entity.VaccinationStatus = obj.VaccinationStatus;
        entity.Adopted = obj.Adopted;

        dbContext.SaveChanges();
    }

    public void Add(Animal obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.Animals.Add(obj);
        dbContext.SaveChanges();
    }
}