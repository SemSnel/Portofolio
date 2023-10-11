using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Authorisations;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IAuthorizer<TRequest>>? _authorizors;
    private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> _logger;

    public AuthorizationBehaviour(IEnumerable<IAuthorizer<TRequest>>? authorizors, ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger)
    {
        _authorizors = authorizors;
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_authorizors is null)
        {
            return await next();
        }

        var context = new AuthorizationContext<TRequest>(request);

        var authorizationTasks = _authorizors
            .Select(async validator =>
                await validator
                .AuthorizeAsync(context, cancellationToken));

        var authorizationResults = await Task
            .WhenAll(authorizationTasks);

        var isValid = authorizationResults
            .All(authorizationResult => authorizationResult.IsAuthorized);
        
        if (isValid)
        {
            _logger.LogInformation("Request {Name} is authorized", typeof(TRequest).Name);
            
            return await next();
        }
        
        var errors = authorizationResults
            .SelectMany(authorizationResult => authorizationResult.Errors)
            .Select(validationFailure => 
                Error.Forbidden(
                validationFailure,
                validationFailure))
            .ToList();
        
        _logger.LogWarning("Request {Name} is not authorized", typeof(TRequest).Name);
        
        // log the errors

        foreach (var error in errors)
        {
            _logger.LogWarning(error.Description);
        }

        return (dynamic) errors;
    }
}