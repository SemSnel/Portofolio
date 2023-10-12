using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

namespace SemSnel.Portofolio.Server.Common.Idempotency;

public sealed class IdempotencyFilterAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var idempotencyService = serviceProvider.GetRequiredService<IIdempotencyService>();
        var idempotencySettings = serviceProvider.GetRequiredService<IOptions<IdempotencySettings>>().Value;
        var dateTimeProvider = serviceProvider.GetRequiredService<IDateTimeProvider>();

        var idempotencyKey = context
            .HttpContext
            .Request
            .Headers[idempotencySettings.HeaderName]
            .FirstOrDefault();
        
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            var errorMessage = $"{idempotencySettings.HeaderName} header is missing";
            
            var badRequest = new BadRequestObjectResult(errorMessage);
            
            context.Result = badRequest;
            
            return;
        }
        
        if (!Guid.TryParse(idempotencyKey, out var idempotencyKeyGuid))
        {
            var errorMessage = $"{idempotencySettings.HeaderName} header is not a valid guid";
            
            var badRequest = new BadRequestObjectResult(errorMessage);
            
            context.Result = badRequest;
            
            return;
        }
        
        var errorOr = await idempotencyService.Exists(idempotencyKeyGuid);

        var exist = errorOr.Value;
        
        if (exist)
        {
            var badRequest = new BadRequestObjectResult("Request already exists");
            
            context.Result = badRequest;
            
            return;
        }
        
        var request = new IdempotentRequest()
        {
            Id = idempotencyKeyGuid,
            Name = context.ActionDescriptor.DisplayName ?? string.Empty,
            CreatedOn = dateTimeProvider.Now(),
        };
            
        await idempotencyService.SaveRequest(request);
        
        await next();
    }
}