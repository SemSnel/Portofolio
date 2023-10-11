using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustHaveRoleRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly string _role;

    public UserMustHaveRoleRule(ICurrentUserService currentUserService, string role)
    {
        _currentUserService = currentUserService;
        _role = role;
    }

    public async Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasRole = _currentUserService.HasRole(_role);

        if (hasRole)
        {
            return new AuthorizationResult()
            {
                IsAuthorized = true
            };
        }

        return new AuthorizationResult()
        {
            IsAuthorized = false,
            Errors = new List<string>()
            {
                $"User does not have role {_role}"
            }
        };
    }
}