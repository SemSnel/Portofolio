using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.UnHandledExceptions;

public class UnHandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<UnHandledExceptionBehaviour<TRequest, TResponse>> _logger;

    public UnHandledExceptionBehaviour(ILogger<UnHandledExceptionBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred");

            return (dynamic)Error.Unexpected();
        }
    }
}