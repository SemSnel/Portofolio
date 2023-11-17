namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;

/// <summary>
/// An authorizer.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public interface IAuthorizer<T>
{
    /// <summary>
    /// Authorizes an item.
    /// </summary>
    /// <param name="context"> The context. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The authorization result. </returns>
    Task<AuthorizationResult> AuthorizeAsync(AuthorizationContext<T> context, CancellationToken cancellationToken);
}