using SemSnel.Portofolio.Domain.Identity;
using SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;

/// <summary>
/// Extensions for <see cref="AbstractAuthorizer{T}"/>.
/// </summary>
public static class AbstractAuthorizorExtensions
{
    /// <summary>
    /// Adds a rule.
    /// </summary>
    /// <param name="authorizer"> The authorizer. </param>
    /// <param name="rule"> The rule. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The authorizer with the rule added. </returns>
    public static AbstractAuthorizer<T> AddRule<T>(this AbstractAuthorizer<T> authorizer, IAuthorizationRule<T> rule)
    {
        authorizer.AddRule(rule);

        return authorizer;
    }

    /// <summary>
    /// Adds a rule that checks if the user is authenticated.
    /// </summary>
    /// <param name="authorizer"> The authorizer. </param>
    /// <param name="currentIdentityService"> The current identity service. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The rule. </returns>
    public static IAuthorizationRule<T> AddMustBeAuthenticatedRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentIdentityService currentIdentityService)
    {
        var rule = new UserMustBeAuthenticatedRule<T>(currentIdentityService);

        authorizer.AddRule(rule);

        return rule;
    }

    /// <summary>
    /// Adds a rule that checks if the user has a role.
    /// </summary>
    /// <param name="authorizer"> The authorizer. </param>
    /// <param name="currentIdentityService"> The current identity service. </param>
    /// <param name="role"> The role. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The rule. </returns>
    public static IAuthorizationRule<T> AddMustHaveRoleRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentIdentityService currentIdentityService, string role)
    {
        var rule = new UserMustHaveRoleRule<T>(currentIdentityService, role);

        authorizer.AddRule(rule);

        return rule;
    }

    /// <summary>
    /// Adds a rule that checks if the user has a permission.
    /// </summary>
    /// <param name="authorizer"> The authorizer. </param>
    /// <param name="currentIdentityService"> The current identity service. </param>
    /// <param name="permission"> The permission. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The rule. </returns>
    public static IAuthorizationRule<T> AddMustHavePermissionRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentIdentityService currentIdentityService, Permission permission)
    {
        var rule = new UserMustHavePermissionRule<T>(currentIdentityService, permission);

        authorizer.AddRule(rule);

        return rule;
    }

    /// <summary>
    /// Adds a rule that checks if the user has a claim.
    /// </summary>
    /// <param name="authorizer"> The authorizer. </param>
    /// <param name="currentIdentityService"> The current identity service. </param>
    /// <param name="claimType"> The claim type. </param>
    /// <param name="claimValue"> The claim value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The rule. </returns>
    public static IAuthorizationRule<T> AddMustHaveClaimRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentIdentityService currentIdentityService, string claimType, string claimValue)
    {
        var rule = new UserHasClaimRule<T>(currentIdentityService, claimType, claimValue);

        authorizer.AddRule(rule);

        return rule;
    }
}