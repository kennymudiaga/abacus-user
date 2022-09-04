using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupCommandHandler : IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    private readonly IMongoDbContext<UserProfile> dbContext;
    public SignupCommandHandler(IMongoDbContext<UserProfile> dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        await dbContext.Collection.InsertOneAsync(user, cancellationToken: cancellationToken);
        return new Result<UserProfile>(user);
    }
}
