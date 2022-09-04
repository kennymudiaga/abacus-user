using AbacusUser.Data.Handlers.Commands;
using AbacusUser.Domain.Models;
using AbacusUser.Domain.Validators;
using FluentValidation;
using MediatR;

namespace AbacusUser.Web.ServiceExtensions;

public static class ApplicationServicesExtension
{
    /// <summary>
    /// Add custom application services to the DI container
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection - this allows for chaining</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program), typeof(SignupHandler), typeof(DbEntity));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DbEntity).Assembly);
        return services;
    }
}
