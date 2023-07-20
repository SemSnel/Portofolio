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
    private readonly IUser _user;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, IUser user, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _user = user;
        _dateTimeProvider = dateTimeProvider;
    }


    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = _user.Id is not null ? "Anonymous" : _user.Id.ToString();
        
        var now = _dateTimeProvider.Now;
        
        _logger.LogInformation(
            "Handling {RequestName} for user {UserId} at {Now}",
            request.GetType().Name,
            userId,
            now);
        
        return next();
    }
}