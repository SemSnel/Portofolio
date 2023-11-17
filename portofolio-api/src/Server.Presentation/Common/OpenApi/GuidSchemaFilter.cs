using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

public sealed class GuidSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(Guid))
        {
            return;
        }

        schema.Example = new OpenApiString(Guid.NewGuid().ToString());
    }
}