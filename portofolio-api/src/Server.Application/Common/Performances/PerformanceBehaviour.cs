using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Performances;

/// <summary>
/// A pipeline behaviour to log the performance of a request.
/// </summary>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
/// <param name="currentIdentityService"> The <see cref="ICurrentIdentityService"/>. </param>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public class PerformanceBehaviour<TRequest, TResponse>(ILogger<PerformanceBehaviour<TRequest, TResponse>> logger,
        ICurrentIdentityService currentIdentityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const int LoggingThreshold = 500;
    private readonly Stopwatch _timer = new ();

    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        var requestName = typeof(TRequest).Name;
        var userId = currentIdentityService.Id is not null ? currentIdentityService.Id.ToString() : string.Empty;
        var userName = string.Empty;

        if (elapsedMilliseconds <= LoggingThreshold)
        {
            logger.LogInformation(
                "CleanArchitecture Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName,
                elapsedMilliseconds,
                userId,
                userName,
                request);

            return response;
        }

        logger.LogWarning(
            "CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            requestName,
            elapsedMilliseconds,
            userId,
            userName,
            request);

        return response;
    }
}