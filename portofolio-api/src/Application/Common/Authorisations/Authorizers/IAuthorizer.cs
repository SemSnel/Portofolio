namespace SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;

public interface IAuthorizer<T>
{
    Task<AuthorizationResult> AuthorizeAsync(AuthorizationContext<T> context, CancellationToken cancellationToken);
}