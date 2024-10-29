namespace AF.Core.Exceptions;

public class EntityDoesNotExistException : Exception
{
    public EntityDoesNotExistException() : this("Entity does not exist.")
    {
    }

    public EntityDoesNotExistException(string message) : base(message)
    {
    }
}