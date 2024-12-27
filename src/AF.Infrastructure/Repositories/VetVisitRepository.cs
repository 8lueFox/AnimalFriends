using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class VetVisitRepository(AfDbContext dbContext): IVetVisitRepository
{
    public IQueryable<VetVisit> Items => dbContext.VetVisits;
    
    public VetVisit? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(VetVisit obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.AnimalId = obj.AnimalId;
        entity.VetName = obj.VetName;
        entity.VisitDate = obj.VisitDate;
        entity.Diagnosis = obj.Diagnosis;
        entity.Treatment = obj.Treatment;

        dbContext.SaveChanges();
    }

    public void Add(VetVisit obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.VetVisits.Add(obj);
        dbContext.SaveChanges();
    }
}