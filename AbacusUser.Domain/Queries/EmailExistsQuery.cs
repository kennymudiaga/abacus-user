using AbacusUser.Domain.Models;
using MediatR;

namespace AbacusUser.Domain.Queries;

public class EmailExistsQuery : IRequest<Result<bool>>
{
    public string? Email { get; set; }
}
