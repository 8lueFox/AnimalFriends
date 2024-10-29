namespace AF.Core.Database.Repositories;

public interface IRepositoryBase<T>
    where T : class, IHasId
{
    IQueryable<T> Items { get; }
    T? GetById(Guid id);
    void Update(T obj);
    void Add(T obj);
}