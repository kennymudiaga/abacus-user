namespace AbacusUser.Domain.Exceptions;

/// <summary>
/// Exception thrown when an API or service call returns unauthorized (401) or an equivalent error.
/// </summary>
public class UnauthorizedException : BusinessException
{
    public UnauthorizedException() : base("Unauthorized") { }
    public UnauthorizedException(string message) : base(message) { }
}
