using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace AbacusUser.Web.ServiceExtensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services) => 
        services.AddEndpointsApiExplorer()
        .AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AbacusUser.Web", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the word 'Bearer', followed by space and JWT",
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
            c.EnableAnnotations();
            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
}
