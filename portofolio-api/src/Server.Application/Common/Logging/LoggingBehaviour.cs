using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.DateTime;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Logging;

/// <summary>
/// A logging behaviour.
/// </summary>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
/// <param name="currentIdentityService"> The <see cref="ICurrentIdentityService"/>. </param>
/// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public sealed class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger,
        ICurrentIdentityService currentIdentityService, IDateTimeProvider dateTimeProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    /// <inheritdoc/>
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = currentIdentityService.Id is not null ? "Anonymous" : currentIdentityService.Id.ToString();

        var now = dateTimeProvider.Now;

        logger.LogInformation(
            "Handling {RequestName} for user {UserId} at {Now}",
            request.GetType().Name,
            userId,
            now);

        return next();
    }
}