using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Server.Application.Common.DateTime;
using SemSnel.Portofolio.Server.Common.Monads;

namespace SemSnel.Portofolio.Server.Common.Idempotency;

/// <summary>
/// An attribute for handling idempotent requests.
/// </summary>
public sealed class IdempotencyFilterAttribute : Attribute, IAsyncActionFilter
{
    /// <inheritdoc/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var idempotencyService = serviceProvider.GetRequiredService<IIdempotencyService>();
        var idempotencySettings = serviceProvider.GetRequiredService<IOptions<IdempotencySettings>>().Value;
        var dateTimeProvider = serviceProvider.GetRequiredService<IDateTimeProvider>();

        var idempotencyKey = context.HttpContext.Request.Headers[idempotencySettings.HeaderName];

        if (string.IsNullOrEmpty(idempotencyKey) || !Guid.TryParse(idempotencyKey, out var idempotencyGuid))
        {
            context.Result = new BadRequestResult();

            return;
        }

        var errorOrIdempotencyKey = await idempotencyService.GetRequest(idempotencyGuid);

        if (errorOrIdempotencyKey.IsError)
        {
            context.Result = errorOrIdempotencyKey.ToOkActionResult();

            return;
        }

        var idempotencyRequest = errorOrIdempotencyKey.Value;

        if (idempotencyRequest.IsSome)
        {
            HandleExistingIdempotentRequest(context, idempotencyRequest!);

            return;
        }

        await HandleNewIdempotentRequest(context, next, idempotencyService, idempotencyGuid, dateTimeProvider);
    }

    private async Task HandleNewIdempotentRequest(ActionExecutingContext context, ActionExecutionDelegate next, IIdempotencyService idempotencyService, Guid idempotencyGuid, IDateTimeProvider dateTimeProvider)
    {
        var result = await next();

        if (result.Exception != null || !(result.Result is ObjectResult response))
        {
            context.Result = new ConflictResult();
            return;
        }

        if (response.StatusCode is null or < 200 or > 299)
        {
            return;
        }

        var responseBody = response.Value == null
            ? null
            : JsonSerializer.Serialize(response.Value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var createdIdempotentRequest = new IdempotentRequest
        {
            Id = idempotencyGuid,
            CreatedOn = dateTimeProvider.Now(),
            RequestId = context.HttpContext.TraceIdentifier,
            RequestBody = context.HttpContext.Request.Body.ToString(),
            RequestHeaders = context.HttpContext.Request.Headers.ToString(),
            ResponseBody = responseBody,
            ResponseStatusCode = response.StatusCode ?? StatusCodes.Status200OK,
        };

        await idempotencyService.SaveRequest(createdIdempotentRequest);

        context.Result = response;
    }

    private void HandleExistingIdempotentRequest(ActionExecutingContext context, IdempotentRequest idempotencyRequest)
    {
        context.Result = new ContentResult
        {
            Content = idempotencyRequest.ResponseBody,
            StatusCode = idempotencyRequest.ResponseStatusCode,
            ContentType = "application/json",
        };
    }
}