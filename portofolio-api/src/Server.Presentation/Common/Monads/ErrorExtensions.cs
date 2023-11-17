using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Common.Monads;

/// <summary>
/// Extension methods for <see cref="ErrorOr"/> to make it easier to use in controllers.
/// </summary>
public static class ErrorExtensions
{
    /// <summary>
    /// Maps the <see cref="ErrorOr{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="errors"> The <see cref="ErrorOr{T}"/>. </param>
    /// <returns> The <see cref="IActionResult"/>. </returns>
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