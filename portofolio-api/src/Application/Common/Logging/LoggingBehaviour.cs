using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Logging;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, ICurrentUserService currentUserService, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
    }


    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.Id is not null ? "Anonymous" : _currentUserService.Id.ToString();
        
        var now = _dateTimeProvider.Now;
        
        _logger.LogInformation(
            "Handling {RequestName} for user {UserId} at {Now}",
            request.GetType().Name,
            userId,
            now);
        
        return next();
    }
}