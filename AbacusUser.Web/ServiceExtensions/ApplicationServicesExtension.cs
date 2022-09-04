using AbacusUser.Data;
using AbacusUser.Data.Handlers.Commands;
using AbacusUser.Domain.Models;
using AbacusUser.Domain.Validators;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace AbacusUser.Web.ServiceExtensions;

public static class ApplicationServicesExtension
{
    /// <summary>
    /// Add custom application services to the DI container
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection - this allows for chaining</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Configure mongodb
        services.AddSingleton(provider => configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>());
        services.AddSingleton<IMongoClient>(provider =>
        {
            var connectionString = provider.GetService<MongoDbConfig>()?.ConnectionString ?? throw new ArgumentException("Invalid mongo-db config: connection string not found!");
            return new MongoClient(connectionString);
        });
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddMediatR(typeof(Program), typeof(SignupCommandHandler), typeof(DbEntity));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DbEntity).Assembly);
        return services;
    }
}
