using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustBeAuthenticatedRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUser _currentUser;

    public UserMustBeAuthenticatedRule(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public AuthorizationResult Evaluate(AuthorizationContext<T> context)
    {
        var isUserAuthenticated = _currentUser.IsAuthenticated();

        if (isUserAuthenticated)
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
                "User is not authenticated"
            }
        };
    }
}