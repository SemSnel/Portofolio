namespace SemSnel.Portofolio.Server.Application.Users;

/// <summary>
/// Fake implementation of <see cref="ICurrentIdentityService"/>.
/// </summary>
public sealed class FakeCurrentIdentityService : ICurrentIdentityService
{
    /// <summary>
    /// Gets the current user id.
    /// </summary>
    public Guid? Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Gets the current user name.
    /// </summary>
    public string? Name { get; }
}