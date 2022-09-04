using AbacusUser.Domain.Models;
using AbacusUser.Domain.Models.Users;
using AbacusUser.Domain.Queries;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
            var exists = await dbContext.Collection.AsQueryable().AnyAsync(x => x.Email == request.Email.ToLower(), cancellationToken);
            return new(exists);                  
        }
    }
}
