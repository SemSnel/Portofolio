namespace SemSnel.Portofolio.Application.Common.Authorisations;

public interface IAuthorizationRule<T>
{
    Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context);
}