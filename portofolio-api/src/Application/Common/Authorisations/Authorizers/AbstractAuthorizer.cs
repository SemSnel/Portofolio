namespace SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;

public abstract class AbstractAuthorizer<T> : IAuthorizer<T>
{
    private List<IAuthorizationRule<T>> Rules { get; } = new List<IAuthorizationRule<T>>();

    public AbstractAuthorizer<T> AddRule(IAuthorizationRule<T> rule)
    {
        Rules.Add(rule);
        
        return this;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(AuthorizationContext<T> context,
        CancellationToken cancellationToken)
    {
        foreach (var rule in Rules)
        {
            var ruleResult = await rule.EvaluateAsync(context);
            
            if (ruleResult.IsAuthorized)
                continue;

            return
                new AuthorizationResult()
                {
                    IsAuthorized = false,
                    Errors = ruleResult.Errors
                };
        }

        return
            new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = new List<string>()
            };
    }
}