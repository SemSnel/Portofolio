using SemSnel.Portofolio.Infrastructure.Common.Authentication;
using SemSnel.Portofolio.Infrastructure.Common.Authorization;
using SemSnel.Portofolio.Server.Common.OpenApi;
using SemSnel.Portofolio.Server.Common.Versioning;
using SemSnel.Portofolio.Server.Filters;

namespace SemSnel.Portofolio.Server;

public static class ConfigureServices
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration configuration)
    {
        // ApiVersioning
        services
            .AddVersioning(configuration)
            .AddOpenApi(configuration);
        
                // Controllers
        services
            .AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });

        return services;
    }
    
    
    public static IApplicationBuilder UseServer(this IApplicationBuilder app)
    {
        app.UseRouting();

        app
            .UseVersioning()
            .UseOpenApi();
        
        app.UseStaticFiles();
        
        app.UseHttpsRedirection();

        app
            .UseAuthenticationServices()
            .UseAuthorizationServices();

        // Controllers and Endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}