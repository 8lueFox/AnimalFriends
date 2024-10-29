namespace AF.Core.Common;

public interface IHasId
{
    Guid Id { get; }
}

public interface IHasName
{
    string Name { get; }
}

public interface IHasIdWithName : IHasId, IHasName
{
}