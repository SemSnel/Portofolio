using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Rules;

public sealed class UserHasClaimRule<T> : IAuthorizationRule<T>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly string _claimType;
    private readonly string _claimValue;
    
    public UserHasClaimRule(ICurrentUserService currentUserService, string claimType, string claimValue)
    {
        _currentUserService = currentUserService;
        _claimType = claimType;
        _claimValue = claimValue;
    }
    
    public async Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasClaim = _currentUserService.HasClaim(_claimType, _claimValue);

        if (hasClaim)
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
                $"User does not have claim {_claimType} with value {_claimValue}"
            }
        };
    }
}