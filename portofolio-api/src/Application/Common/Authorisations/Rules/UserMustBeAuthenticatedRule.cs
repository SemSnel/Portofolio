using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustBeAuthenticatedRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUserService _currentUserService;

    public UserMustBeAuthenticatedRule(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var isUserAuthenticated = _currentUserService.IsAuthenticated();

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