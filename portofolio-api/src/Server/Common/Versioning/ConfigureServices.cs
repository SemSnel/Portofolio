using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SemSnel.Portofolio.Server.Common.Versioning;

public static class ConfigureServices
{
    public static IServiceCollection AddVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        // ApiVersioning
        services
            .AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;

                config.ApiVersionReader = ApiVersionReader
                    .Combine(
                        new HeaderApiVersionReader("x-api-version"),
                        new MediaTypeApiVersionReader("x-api-version"),
                        new QueryStringApiVersionReader("apiVersion"));
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddEndpointsApiExplorer();
        

        return services;
    }
    
    
    public static IApplicationBuilder UseVersioning(this IApplicationBuilder app)
    {
        // ApiVersioning
        app.UseApiVersioning();
        
        return app;
    }
}