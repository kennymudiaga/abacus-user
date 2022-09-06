using AbacusUser.Domain.Contracts;
using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupCommandHandler : MongoDbHandler<UserProfile>, IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    private readonly IPasswordService _passwordService;
    public SignupCommandHandler(IMongoDbContext dbContext, IPasswordService passwordService) : base(dbContext)
    {
        _passwordService = passwordService;
    }

    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        user.SetPassword(_passwordService.HashPassword(user.Email, request.Password ?? ""));
        await Collection.InsertOneAsync(user, cancellationToken: cancellationToken);
        return new Result<UserProfile>(user);
    }
}
