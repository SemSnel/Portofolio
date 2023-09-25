namespace SemSnel.Portofolio.Application.Common.Authorisations.Authorizors;

public abstract class AbstractAuthorizor<T> : IAuthorizor<T>
{
    private List<IAuthorizationRule<T>> Rules { get; } = new List<IAuthorizationRule<T>>();

    public AbstractAuthorizor<T> AddRule(IAuthorizationRule<T> rule)
    {
        Rules.Add(rule);
        
        return this;
    }

    public Task<AuthorizationResult> AuthorizeAsync(AuthorizationContext<T> context,
        CancellationToken cancellationToken)
    {
        foreach (var rule in Rules)
        {
            var ruleResult = rule.Evaluate(context);
            
            if (ruleResult.IsAuthorized)
                continue;
            
            return Task.FromResult(
                new AuthorizationResult()
                {
                    IsAuthorized = false,
                    Errors = ruleResult.Errors
                });
        }

        return Task.FromResult(
            new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = new List<string>()
            });
    }
}