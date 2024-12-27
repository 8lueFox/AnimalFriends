using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class ShelterUserRepository(AfDbContext dbContext): IShelterUserRepository
{
    public IQueryable<ShelterUser> Items => dbContext.ShelterUsers;
    
    public ShelterUser? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(ShelterUser obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.UserId = obj.UserId;
        entity.ShelterId = obj.ShelterId;
        entity.StarDate = obj.StarDate;
        entity.IsOwner = obj.IsOwner;
        entity.IsAdmin = obj.IsAdmin;

        dbContext.SaveChanges();
    }

    public void Add(ShelterUser obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.ShelterUsers.Add(obj);
        dbContext.SaveChanges();
    }
}