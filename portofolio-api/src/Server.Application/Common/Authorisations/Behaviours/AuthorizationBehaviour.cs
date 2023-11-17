using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Behaviours;

/// <summary>
/// The authorization behaviour.
/// The request will be authorized.
/// </summary>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IAuthorizer<TRequest>>? _authorizors;
    private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="authorizors"> The authorizors. </param>
    /// <param name="logger"> The logger. </param>
    public AuthorizationBehaviour(IEnumerable<IAuthorizer<TRequest>>? authorizors, ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger)
    {
        _authorizors = authorizors;
        _logger = logger;
    }

    /// <inheritdoc/>
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

        foreach (var error in errors)
        {
            _logger.LogWarning("Error: {Error}", error);
        }

        return (dynamic)errors;
    }
}