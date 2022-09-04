using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Query
{
    public class EmailExistsQueryHandler : IRequestHandler<EmailExistsQuery, Result<bool>>
    {
        private readonly IMongoDbContext<UserProfile> dbContext;

        public EmailExistsQueryHandler(IMongoDbContext<UserProfile> dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(EmailExistsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email)) return new(false);
            var exists = await dbContext.Query().AnyAsync(x => x.Email == request.Email.ToLower(), cancellationToken);
            return new Result<bool>(exists);                  
        }
    }
}
