using AF.Core.Database.Entities;

namespace AF.Core.Database.Repositories;

public interface IShelterUserRepository
{
    IQueryable<ShelterUser> Items { get; }
    ShelterUser? GetById(Guid userId, Guid shelterId);
    void Update(ShelterUser obj);
    void Add(ShelterUser obj);
}