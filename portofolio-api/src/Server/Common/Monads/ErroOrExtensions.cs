using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Common.Monads;

/// <summary>
/// Extension methods for <see cref="ErrorOr"/> to make it easier to use in controllers.
/// It is used to convert to <see cref="IActionResult"/> and <see cref="IActionResult{T}"/>.
/// </summary>
public static class ErroOrExtensions
{
    public static IActionResult ToOkActionResult<T>(this ErrorOr<T> errorOr)
    {

        return errorOr.Match<IActionResult>(
            value => new OkObjectResult(value),
            errors => errors.ToErrorActionResult());
    }

    private static IActionResult ToErrorActionResult(this IEnumerable<Error> errors)
    {
        var firstError = errors.First();
        
        return firstError.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(firstError.Description),
            ErrorType.Conflict => new ConflictObjectResult(firstError.Description),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(firstError.Description),
            ErrorType.Forbidden => new StatusCodeResult(StatusCodes.Status403Forbidden),
            ErrorType.Validation => new BadRequestObjectResult(firstError.Description),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }
}