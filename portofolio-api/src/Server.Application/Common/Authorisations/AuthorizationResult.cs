namespace SemSnel.Portofolio.Server.Application.Common.Authorisations;

public record AuthorizationResult
{
    /// <summary>
    /// Gets a value indicating whether the user is authorized.
    /// </summary>
    public bool IsAuthorized { get; init; }

    /// <summary>
    /// Gets the errors.
    /// </summary>
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
}