using SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;

/// <summary>
/// An abstract authorizer.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public abstract class AbstractAuthorizer<T> : IAuthorizer<T>
{
    private List<IAuthorizationRule<T>> Rules { get; } = new List<IAuthorizationRule<T>>();

    /// <summary>
    /// Adds a rule.
    /// </summary>
    /// <param name="rule"> The rule. </param>
    /// <returns> The authorizer. </returns>
    public AbstractAuthorizer<T> AddRule(IAuthorizationRule<T> rule)
    {
        Rules.Add(rule);

        return this;
    }

    /// <inheritdoc/>
    public async Task<AuthorizationResult> AuthorizeAsync(
        AuthorizationContext<T> context,
        CancellationToken cancellationToken)
    {
        foreach (var rule in Rules)
        {
            var ruleResult = await rule.EvaluateAsync(context);

            if (ruleResult.IsAuthorized)
            {
                continue;
            }

            return
                new AuthorizationResult()
                {
                    IsAuthorized = false,
                    Errors = ruleResult.Errors,
                };
        }

        return
            new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = new List<string>(),
            };
    }
}