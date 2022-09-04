using AbacusUser.Domain.Commands;
using FluentValidation;

namespace AbacusUser.Domain.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private static readonly string[] genders = {"male", "female"};
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);       
        RuleFor(x => x.Email).EmailAddress().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.OtherNames).MaximumLength(50);
        RuleFor(x => x.Gender).NotEmpty().MaximumLength(10).Must(x =>
         genders.Contains(x, StringComparer.OrdinalIgnoreCase)).WithMessage("Gender must be 'Male' or 'Female'");
        RuleFor(x => x.Phone).MaximumLength(20);
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}
