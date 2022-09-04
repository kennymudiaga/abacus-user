using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Query;

public class EmailExistsQueryHandler : MongoDbHandler<UserProfile>, IRequestHandler<EmailExistsQuery, Result<bool>>
{
    public EmailExistsQueryHandler(IMongoDbContext dbContext) : base(dbContext) { }

    public async Task<Result<bool>> Handle(EmailExistsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email)) return new(false);
        var exists = await Query.AnyAsync(x => x.Email == request.Email.ToLower(), cancellationToken);
        return new Result<bool>(exists);                  
    }
}
