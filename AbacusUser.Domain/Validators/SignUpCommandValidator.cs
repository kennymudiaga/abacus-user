using AbacusUser.Domain.Commands;
using AbacusUser.Domain.Queries;
using FluentValidation;
using MediatR;

namespace AbacusUser.Domain.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private static readonly string[] genders = {"male", "female"};
    public SignUpCommandValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);       
        RuleFor(x => x.Email).EmailAddress().MaximumLength(50)
            .CustomAsync(async (email, context, cancelToken) =>
            {
                var result = await mediator.Send(new EmailExistsQuery { Email = email }, cancelToken);
                if (result.Success && result.Data)
                {
                    context.AddFailure(nameof(SignUpCommand.Email), $"The email address {email} is already in use.");
                }
            });
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.OtherNames).MaximumLength(50);
        RuleFor(x => x.Gender).NotEmpty().MaximumLength(10).Must(x =>
         genders.Contains(x, StringComparer.OrdinalIgnoreCase)).WithMessage("Gender must be 'Male' or 'Female'");
        RuleFor(x => x.Phone).MaximumLength(20);
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("User must be 18+");
    }
}
