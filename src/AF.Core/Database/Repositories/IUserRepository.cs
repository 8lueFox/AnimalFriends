using AF.Core.Database.Entities;

namespace AF.Core.Database.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    void SaveChanges();
}