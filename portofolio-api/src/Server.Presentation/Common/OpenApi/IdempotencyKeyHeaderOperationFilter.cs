using System;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SemSnel.Portofolio.Server.Common.OpenApi;

public sealed class IdempotencyKeyHeaderOperationFilter : IOperationFilter
{
    private readonly IdempotencySettings _idempotencySettings;

    public IdempotencyKeyHeaderOperationFilter(IOptions<IdempotencySettings> idempotencySettings)
    {
        _idempotencySettings = idempotencySettings.Value;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            return;
        }

        if (context.ApiDescription.HttpMethod != "POST")
        {
            return;
        }

        var idempotencyKeyParameter = new OpenApiParameter
        {
            Name = _idempotencySettings.HeaderName,
            In = ParameterLocation.Header,
            Description = "Idempotency key",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Format = "uuid",
                Example = new OpenApiString(Guid.NewGuid().ToString()),
            },
        };

        operation
            .Parameters
            .Add(idempotencyKeyParameter);
    }
}