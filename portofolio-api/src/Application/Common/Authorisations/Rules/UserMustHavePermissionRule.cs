using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustHavePermissionRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUser _currentUser;
    private readonly string _permission;

    public UserMustHavePermissionRule(ICurrentUser currentUser, string permission)
    {
        _currentUser = currentUser;
        _permission = permission;
    }

    public AuthorizationResult Evaluate(AuthorizationContext<T> context)
    {
        var hasPermission = _currentUser.HasPermission(_permission);

        if (hasPermission)
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
                $"User does not have permission {_permission}"
            }
        };
    }
}