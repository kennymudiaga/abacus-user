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
        return services;
    }
}
