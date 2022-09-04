using AbacusUser.Domain.Models;
using AbacusUser.Domain.Models.Users;
using MediatR;

namespace AbacusUser.Domain.Commands
{
    public class SignUpCommand : IRequest<Result<UserProfile>>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherNames { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
