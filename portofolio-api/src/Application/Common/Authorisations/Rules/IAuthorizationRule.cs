namespace SemSnel.Portofolio.Application.Common.Authorisations;

public interface IAuthorizationRule<T>
{
    AuthorizationResult Evaluate(AuthorizationContext<T> context);
}