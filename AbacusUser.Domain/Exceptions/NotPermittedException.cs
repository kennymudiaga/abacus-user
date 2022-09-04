namespace AbacusUser.Domain.Exceptions;

public class NotPermittedException : BusinessException
{
    public NotPermittedException() : base("User not permitted") { }
    public NotPermittedException(string message) : base(message) { }
}
