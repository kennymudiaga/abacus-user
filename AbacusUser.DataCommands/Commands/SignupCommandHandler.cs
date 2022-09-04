using AbacusUser.Domain.Commands;
using AbacusUser.Domain.Models;
using AbacusUser.Domain.Models.Users;
using MediatR;
using MongoDB.Driver;

namespace AbacusUser.Data.Handlers.Commands;

public class SignupCommandHandler : IRequestHandler<SignUpCommand, Result<UserProfile>>
{
    private readonly IMongoDatabase dbContext;
    public SignupCommandHandler(IMongoClient mongoClient)
    {
        dbContext = mongoClient.GetDatabase("AbacusUsers");
    }

    public async Task<Result<UserProfile>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new UserProfile(request);
        var collection = dbContext.GetCollection<UserProfile>(nameof(UserProfile));
        await collection.InsertOneAsync(user, cancellationToken: cancellationToken);
        return new Result<UserProfile>(user);
    }
}
