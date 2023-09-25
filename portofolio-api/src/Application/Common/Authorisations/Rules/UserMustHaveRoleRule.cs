using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustHaveRoleRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUser _currentUser;
    private readonly string _role;

    public UserMustHaveRoleRule(ICurrentUser currentUser, string role)
    {
        _currentUser = currentUser;
        _role = role;
    }

    public AuthorizationResult Evaluate(AuthorizationContext<T> context)
    {
        var hasRole = _currentUser.HasRole(_role);

        if (hasRole)
        {
            return new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = new List<string>()
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