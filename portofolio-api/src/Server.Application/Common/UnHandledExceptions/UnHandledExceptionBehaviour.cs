using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Server.Application.Common.UnHandledExceptions;

/// <summary>
/// A pipeline behaviour that logs unhandled exceptions.
/// </summary>
/// <param name="logger"> The logger. </param>
/// <param name="dateTimeProvider"> The date time provider. </param>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public class UnHandledExceptionBehaviour<TRequest, TResponse>(
        ILogger<UnHandledExceptionBehaviour<TRequest, TResponse>> logger, IDateTimeProvider dateTimeProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await next();

            if (!result.IsError)
            {
                return result;
            }

            if (result.Errors == null)
            {
                return result;
            }

            var error = result.Errors.First();

            var code = error.Code;
            var errorType = error.Type;
            var message = error.Description;

            logger.LogError(
                "An error occurred while handling {RequestName} for user {UserId} at {Now}: {Code} {ErrorType} {Message}",
                request.GetType().Name,
                "Anonymous",
                dateTimeProvider.Now(),
                code,
                errorType,
                message);

            return result;
        }
        catch (Exception exception)
        {
            // log the exception
            logger.LogError(exception, "An unhandled exception occurred");

            throw;
        }
    }
}