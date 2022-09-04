using AbacusUser.Domain.Commands;
using AbacusUser.Domain.Models;
using AbacusUser.Domain.Models.Users;
using MediatR;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupHandler : IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        await Task.Delay(200, cancellationToken);
        return new Result<UserProfile>(user);
    }
}
