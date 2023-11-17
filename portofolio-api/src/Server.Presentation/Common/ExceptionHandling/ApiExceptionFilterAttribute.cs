using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SemSnel.Portofolio.Server.Common.ExceptionHandling;

/// <summary>
/// An attribute for handling exceptions.
/// </summary>
public class ApiExceptionFilterAttribute(
    ILogger<ApiExceptionFilterAttribute> logger) : ExceptionFilterAttribute
{
    /// <inheritdoc/>
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "An unhandled exception occurred");

        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

        // if environment is development, add stack trace
        var isDevelopment = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment();

        if (isDevelopment)
        {
            details.Detail = context.Exception.StackTrace;
        }

        context.ExceptionHandled = true;
    }
}