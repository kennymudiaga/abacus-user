namespace AbacusUser.Domain.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException()
        : base("The requested resource does not exist, or you may not have access to it.")
    { }
    public NotFoundException(string message) : base(message) { }
}
