using AbacusUser.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AbacusUser.Domain.Validators
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            List<ValidationFailure> failures = new();
            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);
                failures.AddRange(result?.Errors ?? new());
            }
            if (failures.Count > 0)
            {
                return new() { Errors = failures.Select(x => x.ErrorMessage).ToList() };
            }
            return await next();
        }
    }
}
