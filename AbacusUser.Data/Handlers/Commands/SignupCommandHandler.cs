using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupCommandHandler : MongoDbHandler<UserProfile>, IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    public SignupCommandHandler(IMongoDbContext dbContext) : base(dbContext) { }

    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        await Collection.InsertOneAsync(user, cancellationToken: cancellationToken);
        return new Result<UserProfile>(user);
    }
}
