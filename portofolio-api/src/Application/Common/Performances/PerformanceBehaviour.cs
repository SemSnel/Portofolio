using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.Common.Performances;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const int LoggingThreshold = 500;
    private readonly Stopwatch _timer;
    private readonly ILogger<PerformanceBehaviour<TRequest, TResponse>> _logger;
    private readonly IUser _user;

    public PerformanceBehaviour(
        ILogger<PerformanceBehaviour<TRequest, TResponse>> logger,
        IUser user)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _user = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= LoggingThreshold) 
            return response;
        
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id is not null ? _user.Id.ToString() : string.Empty;
        var userName = string.Empty; // TODO: get user name

        _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            requestName, elapsedMilliseconds, userId, userName, request);

        return response;
    }
}