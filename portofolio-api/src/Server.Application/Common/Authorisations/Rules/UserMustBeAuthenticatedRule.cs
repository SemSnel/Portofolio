using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

/// <summary>
/// A rule that checks if the user is authenticated.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public sealed class UserMustBeAuthenticatedRule<T>(ICurrentIdentityService currentIdentityService) : IAuthorizationRule<T>
{
    /// <inheritdoc/>
    public Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var isUserAuthenticated = currentIdentityService.IsAuthenticated();

        if (isUserAuthenticated)
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
                "User is not authenticated",
            },
        });
    }
}