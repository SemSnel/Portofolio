using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserMustHavePermissionRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly string _permission;

    public UserMustHavePermissionRule(ICurrentUserService currentUserService, string permission)
    {
        _currentUserService = currentUserService;
        _permission = permission;
    }

    public async Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasPermission = _currentUserService.HasPermission(_permission);

        if (hasPermission)
        {
            return new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = Enumerable.Empty<string>()
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