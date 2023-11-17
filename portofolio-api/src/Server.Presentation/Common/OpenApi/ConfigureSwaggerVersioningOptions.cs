using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

/// <summary>
/// Configure Swagger to use the API versioning information.
/// </summary>
public class ConfigureSwaggerVersioningOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    /// <inheritdoc/>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }

    /// <inheritdoc/>
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    private OpenApiInfo CreateVersionInfo(
        ApiVersionDescription desc)
    {
        var info = new OpenApiInfo()
        {
            Title = ".NET Core (.NET 6) Web API",
            Version = desc.ApiVersion.ToString(),
        };

        if (desc.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        return info;
    }
}