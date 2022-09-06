using AbacusUser.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace AbacusUser.Infrastructure
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<string> passwordHasher;

        public PasswordService(IPasswordHasher<string> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }
        public string HashPassword(string username, string password)
        {
            return passwordHasher.HashPassword(username, password);
        }

        public bool IsValidPassword(string username, string passwordHash, string providedPassword)
        {
            return passwordHasher.VerifyHashedPassword(username, passwordHash, providedPassword)
                != PasswordVerificationResult.Failed;
        }
    }
}