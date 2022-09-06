using AbacusUser.Domain.Models;
using MediatR;

namespace AbacusUser.Domain.Commands;

public class ResetPasswordCommand : IRequest<Result<bool>>
{
    public string? Email { get; set; }
}
