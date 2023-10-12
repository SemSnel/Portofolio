using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SemSnel.Portofolio.Server.Common.Versioning;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

public static class ConfigureServices
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        // Swagger
        services
            .AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.DocumentFilter<SwaggerDefaultPathValues>();
                
                // add idempotency key header
                options.OperationFilter<IdempotencyKeyHeaderOperationFilter>();
                options.SchemaFilter<GuidSchemaFilter>();
            })
            .ConfigureOptions<ConfigureSwaggerVersioningOptions>();
        
        return services;
    }
    
    
    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}