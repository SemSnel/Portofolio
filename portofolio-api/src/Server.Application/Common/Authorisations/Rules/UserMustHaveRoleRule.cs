using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

/// <summary>
/// A rule that checks if the user has a role.
/// </summary>
/// <param name="currentIdentityService"> The current identity service. </param>
/// <param name="role"> The role. </param>
/// <typeparam name="T"> The type. </typeparam>
public sealed class UserMustHaveRoleRule<T>(
        ICurrentIdentityService currentIdentityService,
        string role)
    : IAuthorizationRule<T>
{
    /// <inheritdoc/>
    public Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasRole = currentIdentityService.HasRole(role);

        if (hasRole)
        {
            return Task.FromResult(
                new AuthorizationResult()
            {
                IsAuthorized = true,
            });
        }

        return Task.FromResult(new AuthorizationResult()
        {
            IsAuthorized = false,
            Errors = new List<string>()
            {
                $"User does not have role {role}",
            },
        });
    }
}