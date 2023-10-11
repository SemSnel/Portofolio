using SemSnel.Portofolio.Application.Common.Authorisations.Rules;
using SemSnel.Portofolio.Application.Users;
using SemSnel.Portofolio.Domain.Identity;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;

public static class AbstractAuthorizorExtensions
{
    public static AbstractAuthorizer<T> AddRule<T>(this AbstractAuthorizer<T> authorizer, IAuthorizationRule<T> rule)
    {
        authorizer.AddRule(rule);
        
        return authorizer;
    }
    
    public static IAuthorizationRule<T> AddMustBeAuthenticatedRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentUserService currentUserService)
    {
        var rule = new UserMustBeAuthenticatedRule<T>(currentUserService);
        
        authorizer.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHaveRoleRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentUserService currentUserService, string role)
    {
        var rule = new UserMustHaveRoleRule<T>(currentUserService, role);
        
        authorizer.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHavePermissionRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentUserService currentUserService, Permission permission)
    {
        var rule = new UserMustHavePermissionRule<T>(currentUserService, permission);
        
        authorizer.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHaveClaimRule<T>(this AbstractAuthorizer<T> authorizer, ICurrentUserService currentUserService, string claimType, string claimValue)
    {
        var rule = new UserHasClaimRule<T>(currentUserService, claimType, claimValue);
        
        authorizer.AddRule(rule);
        
        return rule;
    }
}