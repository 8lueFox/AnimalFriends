using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;

namespace AF.Infrastructure.Repositories;

public class UserRepository(AfDbContext dbContext): IUserRepository
{
    public IQueryable<User> Items => dbContext.Users;
    
    public User? GetById(Guid id)
    {
        var item = Items.FirstOrDefault(a => a.Id == id);

        if (item == null)
            throw new ArgumentOutOfRangeException(nameof(id));

        return item;
    }

    public void Update(User obj)
    {
        obj.ThrowIfNull(nameof(obj));

        var entity = GetById(obj.Id);
        
        if (entity == null)
            throw new ArgumentOutOfRangeException(nameof(entity.Id));

        entity.UserName = obj.UserName;
        entity.FirstName = obj.FirstName;
        entity.LastName = obj.LastName;
        entity.Email = obj.Email;
        entity.Phone = obj.Phone;
        entity.Address = obj.Address;
        entity.Gender = obj.Gender;
        entity.Birthday = obj.Birthday;

        dbContext.SaveChanges();
    }

    public void Add(User obj)
    {
        obj.ThrowIfNull(nameof(obj));
        dbContext.Users.Add(obj);
        dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}