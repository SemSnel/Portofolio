using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Common.Monads;

public static class ErrorExtensions
{
    public static IActionResult ToErrorActionResult(this IEnumerable<Error> errors)
    {
        var enumerable = errors as Error[] ?? errors.ToArray();
        
        var firstError = enumerable.First();
        
        return firstError.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(firstError.Description),
            ErrorType.Conflict => new ConflictObjectResult(firstError.Description),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(firstError.Description),
            ErrorType.Forbidden => new StatusCodeResult(StatusCodes.Status403Forbidden),
            ErrorType.Validation => enumerable.ToBadRequestActionResult(),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }
    
    private static IActionResult ToBadRequestActionResult(this IEnumerable<Error> errors)
    {
        var validationErrors = errors
            .Where(error => error.Type == ErrorType.Validation)
            .ToList();

        var problemErrors = validationErrors
            .GroupBy(error => error.Code)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray());

        var problemDetails = new ValidationProblemDetails(problemErrors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "See the errors property for details.",
        };

        return new BadRequestObjectResult(problemDetails);
    }
}