using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Application.Common.Files;
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
    
    public static IActionResult ToCreatedActionResult<T>(this ErrorOr<T> errorOr, string actionName, object? routeValues = null)
    {
        return errorOr.Match<IActionResult>(
            value => new CreatedAtActionResult(actionName, null, routeValues, value),
            errors => errors.ToErrorActionResult());
    }
    
    public static IActionResult ToNoContentActionResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.Match<IActionResult>(
            _ => new NoContentResult(),
            errors => errors.ToErrorActionResult());
    }
    
    public static IActionResult ToFileContentResult(this ErrorOr<FileDto> errorOr)
    {
        return errorOr.Match<IActionResult>(
            value => new FileContentResult(value.Content, value.ContentType) { FileDownloadName = value.Name },
            errors => errors.ToErrorActionResult());
    }
}