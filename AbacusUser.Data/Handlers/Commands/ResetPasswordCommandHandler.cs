using AbacusUser.Domain.Contracts;
using AbacusUser.Domain.Models.Users;

namespace AbacusUser.Data.Handlers.Commands;

public class ResetPasswordCommandHandler : MongoDbHandler<UserProfile>, IRequestHandler<ResetPasswordCommand, Result<bool>>
{
    IPasswordService _passwordService;
    public ResetPasswordCommandHandler(IMongoDbContext dbContext, IPasswordService passwordService) : base(dbContext)
    {
        _passwordService = passwordService;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email?.Trim().ToLower() ?? throw new ArgumentException("Email cannot be empty!");
        var user = await Query.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        if (user == null)
        {
            return new(true);// return success even if the user was not found.
        }
        //TODO: Read expiry timeout from config
        user.GeneratePasswordToken(DateTime.UtcNow.AddMinutes(5));
        var update = new UpdateDefinitionBuilder<UserProfile>()
            .Set(x => x.PasswordToken, user.PasswordToken)
            .Set(x => x.PasswordTokenExpiry, user.PasswordTokenExpiry);
        await Collection.FindOneAndUpdateAsync(x => x.Id == user.Id, update, cancellationToken: cancellationToken);
        return new(true);
    }
}
