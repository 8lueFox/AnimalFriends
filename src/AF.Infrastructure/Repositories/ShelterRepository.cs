using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class ShelterRepository(AfDbContext dbContext) : IShelterRepository
{
    public IQueryable<Shelter> Items => dbContext.Shelters;
    
    public Shelter? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(Shelter obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.Name = obj.Name;
        entity.Address = obj.Address;
        entity.Phone = obj.Phone;
        entity.Email = obj.Email;
        entity.Address = obj.Address;
        entity.BankAccount = obj.BankAccount;

        dbContext.SaveChanges();
    }

    public void Add(Shelter obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.Shelters.Add(obj);
        dbContext.SaveChanges();
    }
}