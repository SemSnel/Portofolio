using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Server.Common.Versioning;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

/// <summary>
/// Configure Swagger to use the API versioning information.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configure Swagger to use the API versioning information.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the configured Swagger. </returns>
    public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.DocumentFilter<SwaggerDefaultPathValues>();

                options.OperationFilter<IdempotencyKeyHeaderOperationFilter>();
                options.SchemaFilter<GuidSchemaFilter>();
            })
            .ConfigureOptions<ConfigureSwaggerVersioningOptions>();

        return services;
    }

    /// <summary>
    /// Open API middleware.
    /// </summary>
    /// <param name="app"> The <see cref="IApplicationBuilder"/>. </param>
    /// <returns> The <see cref="IApplicationBuilder"/> with the configured Swagger. </returns>
    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}