using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Infrastructure.Common.Authentication;
using SemSnel.Portofolio.Infrastructure.Common.Authorization;
using SemSnel.Portofolio.Server.Common.ExceptionHandling;
using SemSnel.Portofolio.Server.Common.OpenApi;
using SemSnel.Portofolio.Server.Common.Versioning;
using Serilog;
using Serilog.Events;

namespace SemSnel.Portofolio.Server;

/// <summary>
/// Configure the server.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Add the server to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the configured server. </returns>
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSignalR();

        services
            .AddVersioning(configuration)
            .AddOpenApi(configuration);

        services
            .AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });

        return services;
    }

    /// <summary>
    /// Use the server.
    /// </summary>
    /// <param name="app"> The <see cref="IApplicationBuilder"/>. </param>
    /// <returns> The <see cref="IApplicationBuilder"/> with the configured server. </returns>
    public static IApplicationBuilder UseServer(this IApplicationBuilder app)
    {
        app.UseRouting();

        app
            .UseVersioning()
            .UseOpenApi();

        app.UseHttpsRedirection();

        app
            .UseAuthenticationServices()
            .UseAuthorizationServices();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}