using System.Linq.Expressions;
using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using Microsoft.EntityFrameworkCore;

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

    public Shelter? GetById(Guid id, params string[] includeStrings)
    {
        var query = includeStrings.Aggregate(Items, (current, includeExpression) => current.Include(includeExpression));

        return query.FirstOrDefault(a => a.Id == id);
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

    public Shelter? GetById(Guid id, params Expression<Func<object, object>>[] includeExpressions)
    {
        throw new NotImplementedException();
    }
}