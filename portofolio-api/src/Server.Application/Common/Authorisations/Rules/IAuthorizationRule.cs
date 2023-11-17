namespace SemSnel.Portofolio.Server.Application.Common.Authorisations.Rules;

/// <summary>
/// An authorization rule.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public interface IAuthorizationRule<T>
{
    /// <summary>
    /// Evaluates the rule.
    /// </summary>
    /// <param name="context"> The context. </param>
    /// <returns> The authorization result. </returns>
    Task<AuthorizationResult> EvaluateAsync(AuthorizationContext<T> context);
}