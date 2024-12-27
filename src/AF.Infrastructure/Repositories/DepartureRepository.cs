using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class DepartureRepository(AfDbContext dbContext) : IDepartureRepository
{
    public IQueryable<Departure> Items => dbContext.Departures;
    
    public Departure? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(Departure obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.AnimalId = obj.AnimalId;
        entity.UserId = obj.UserId;
        entity.DepartureDate = obj.DepartureDate;
        entity.Notes = obj.Notes;

        dbContext.SaveChanges();
    }

    public void Add(Departure obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.Departures.Add(obj);
        dbContext.SaveChanges();
    }
}