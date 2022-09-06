namespace AbacusUser.Domain.Contracts;

public interface IPasswordService
{
    string HashPassword(string username, string password);
    bool IsValidPassword(string username, string passwordHash, string providedPassword);
}
