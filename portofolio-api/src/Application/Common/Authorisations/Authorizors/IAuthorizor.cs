namespace SemSnel.Portofolio.Application.Common.Authorisations;

public interface IAuthorizor<T>
{
    Task<AuthorizationResult> AuthorizeAsync(AuthorizationContext<T> context, CancellationToken cancellationToken);
}