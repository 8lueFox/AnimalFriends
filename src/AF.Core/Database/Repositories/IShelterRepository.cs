using AF.Core.Database.Entities;

namespace AF.Core.Database.Repositories;

public interface IShelterRepository : IRepositoryBase<Shelter>
{
    Shelter? GetById(Guid id, params string[] includeStrings);
}