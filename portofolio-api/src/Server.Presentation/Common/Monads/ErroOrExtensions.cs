using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.Files;

namespace SemSnel.Portofolio.Server.Common.Monads;

/// <summary>
/// Extension methods for <see cref="ErrorOr"/> to make it easier to use in controllers.
/// It is used to convert to <see cref="IActionResult"/> and <see cref="IActionResult{T}"/>.
/// </summary>
public static class ErroOrExtensions
{
    /// <summary>
    /// Maps the <see cref="ErrorOr{T}"/> to an <see cref="IActionResult{T}"/>.
    /// </summary>
    /// <param name="errorOr"> The <see cref="ErrorOr{T}"/>. </param>
    /// <typeparam name="T"> The type of the value. </typeparam>
    /// <returns> The <see cref="IActionResult{T}"/>. </returns>
    public static IActionResult ToOkActionResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.Match<IActionResult>(
            value => new OkObjectResult(value),
            errors => errors.ToErrorActionResult());
    }

    /// <summary>
    /// Maps the <see cref="ErrorOr{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="errorOr"> The <see cref="ErrorOr{T}"/>. </param>
    /// <param name="actionName"> The action name. </param>
    /// <param name="routeValues"> The route values. </param>
    /// <typeparam name="T"> The type of the value. </typeparam>
    /// <returns> The <see cref="IActionResult"/>. </returns>
    public static IActionResult ToCreatedActionResult<T>(this ErrorOr<T> errorOr, string actionName, object? routeValues = null)
    {
        return errorOr.Match<IActionResult>(
            value => new CreatedAtActionResult(actionName, null, routeValues, value),
            errors => errors.ToErrorActionResult());
    }

    /// <summary>
    /// Maps the <see cref="ErrorOr{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="errorOr"> The <see cref="ErrorOr{T}"/>. </param>
    /// <typeparam name="T"> The type of the value. </typeparam>
    /// <returns> The <see cref="IActionResult"/>. </returns>
    public static IActionResult ToNoContentActionResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.Match<IActionResult>(
            _ => new NoContentResult(),
            errors => errors.ToErrorActionResult());
    }

    /// <summary>
    /// Maps the <see cref="ErrorOr{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="errorOr"> The <see cref="ErrorOr{T}"/>. </param>
    /// <returns> The <see cref="IActionResult"/>. </returns>
    public static IActionResult ToFileContentResult(this ErrorOr<FileDto> errorOr)
    {
        return errorOr.Match<IActionResult>(
            value => new FileContentResult(value.Content, value.ContentType) { FileDownloadName = value.Name },
            errors => errors.ToErrorActionResult());
    }
}