namespace UserCard.DAL.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string message)
        : base(message)
    {
    }

    public EntityAlreadyExistsException()
        : base()
    {
    }
}