using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class AdoptionRepository(AfDbContext dbContext) : IAdoptionRepository
{
    public IQueryable<Adoption> Items => dbContext.Adoptions;
    
    public Adoption? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(Adoption obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.AnimalId = obj.AnimalId;
        entity.AdopterId = obj.AdopterId;
        entity.FirstName = obj.FirstName;
        entity.LastName = obj.LastName;
        entity.Address = obj.Address;
        entity.Email = obj.Email;
        entity.Phone = obj.Phone;
        entity.AdoptionDate = obj.AdoptionDate;
        entity.AdoptionStatus = obj.AdoptionStatus;

        dbContext.SaveChanges();
    }

    public void Add(Adoption obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.Adoptions.Add(obj);
        dbContext.SaveChanges();
    }
}