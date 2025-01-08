using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Exceptions;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class ShelterUserRepository(AfDbContext dbContext): IShelterUserRepository
{
    public IQueryable<ShelterUser> Items => dbContext.ShelterUsers;
    
    public ShelterUser GetById(Guid userId, Guid shelterId)
    {
        var item = Items.FirstOrDefault(a => a.UserId == userId && a.ShelterId == shelterId);

        if (item == null)
            throw new EntityDoesNotExistException();

        return item;
    }

    public void Update(ShelterUser obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.UserId, obj.ShelterId);
        
        if (entity == null)
            throw new EntityDoesNotExistException();

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