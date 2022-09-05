using AbacusUser.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupCommandHandler : MongoDbHandler<UserProfile>, IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    private readonly IPasswordHasher<string> _passwordHasher;
    public SignupCommandHandler(IMongoDbContext dbContext, IPasswordHasher<string> passwordHasher) : base(dbContext)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        user.SetPassword(_passwordHasher.HashPassword(user.Email, request.Password));
        await Collection.InsertOneAsync(user, cancellationToken: cancellationToken);
        return new Result<UserProfile>(user);
    }
}
