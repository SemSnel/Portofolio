using SemSnel.Portofolio.Application.Common.Authorisations.Rules;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application.Common.Authorisations.Authorizors;

public static class AbstractAuthorizorExtensions
{
    public static AbstractAuthorizor<T> AddRule<T>(this AbstractAuthorizor<T> authorizor, IAuthorizationRule<T> rule)
    {
        authorizor.AddRule(rule);
        
        return authorizor;
    }
    
    public static IAuthorizationRule<T> AddMustBeAuthenticatedRule<T>(this AbstractAuthorizor<T> authorizor, ICurrentUser currentUser)
    {
        var rule = new UserMustBeAuthenticatedRule<T>(currentUser);
        
        authorizor.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHaveRoleRule<T>(this AbstractAuthorizor<T> authorizor, ICurrentUser currentUser, string role)
    {
        var rule = new UserMustHaveRoleRule<T>(currentUser, role);
        
        authorizor.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHavePermissionRule<T>(this AbstractAuthorizor<T> authorizor, ICurrentUser currentUser, string permission)
    {
        var rule = new UserMustHavePermissionRule<T>(currentUser, permission);
        
        authorizor.AddRule(rule);
        
        return rule;
    }
    
    public static IAuthorizationRule<T> AddMustHaveClaimRule<T>(this AbstractAuthorizor<T> authorizor, ICurrentUser currentUser, string claimType, string claimValue)
    {
        var rule = new UserHasClaimRule<T>(currentUser, claimType, claimValue);
        
        authorizor.AddRule(rule);
        
        return rule;
    }
}