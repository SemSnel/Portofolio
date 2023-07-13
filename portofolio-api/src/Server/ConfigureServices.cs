using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using SemSnel.Portofolio.Server.Common.OpenApi;
using SemSnel.Portofolio.Server.Common.Versioning;
using SemSnel.Portofolio.Server.WeatherForecasts.v1;

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
            .AddControllers();

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

        // Controllers and Endpoints
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        return app;
    }
}