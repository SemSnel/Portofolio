using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

public class SwaggerDefaultPathValues : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = new OpenApiPaths();

        foreach (var (key, value) in swaggerDoc.Paths)
        {
            
            paths.Add(key.Replace("{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
        }
        
        swaggerDoc.Paths = paths;
    }
}