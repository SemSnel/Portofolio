using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

/// <summary>
/// A rule that checks if the user has a permission.
/// </summary>
/// <param name="currentIdentityService"> The current identity service. </param>
/// <param name="permission"> The permission. </param>
/// <typeparam name="T"> The type. </typeparam>
public sealed class UserMustHavePermissionRule<T>(ICurrentIdentityService currentIdentityService, string permission)
    : IAuthorizationRule<T>
{
    /// <inheritdoc/>
    public Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context)
    {
        var hasPermission = currentIdentityService.HasPermission(permission);

        if (hasPermission)
        {
            return Task.FromResult(new AuthorizationResult()
            {
                IsAuthorized = true,
                Errors = Enumerable.Empty<string>(),
            });
        }

        return Task.FromResult(new AuthorizationResult()
        {
            IsAuthorized = false,
            Errors = new List<string>()
            {
                $"User does not have permission {permission}",
            },
        });
    }
}