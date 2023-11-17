using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

/// <summary>
/// A rule that checks if the user has a claim.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public sealed class UserHasClaimRule<T>(
        ICurrentIdentityService currentIdentityService,
        string claimType,
        string claimValue)
    : IAuthorizationRule<T>
{
    /// <inheritdoc/>
    public Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasClaim = currentIdentityService.HasClaim(claimType, claimValue);

        if (hasClaim)
        {
            return Task.FromResult(new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = new List<string>(),
            });
        }

        return Task.FromResult(new AuthorizationResult()
        {
            IsAuthorized = false,
            Errors = new List<string>()
            {
                $"User does not have claim {claimType} with value {claimValue}",
            },
        });
    }
}