namespace SemSnel.Portofolio.Server.Application.Users;

/// <summary>
/// A service to get the current identity.
/// </summary>
public interface ICurrentIdentityService
{
    /// <summary>
    /// Gets the current user id.
    /// </summary>
    public Guid? Id { get; }

    /// <summary>
    /// Gets the current user name.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Checks if the current user is authenticated.
    /// </summary>
    /// <returns> True if the current user is authenticated, false otherwise. </returns>
    public bool IsAuthenticated()
    {
        return Id.HasValue;
    }

    /// <summary>
    /// Checks if the current user is in the given role.
    /// </summary>
    /// <param name="role"> The role to check. </param>
    /// <returns> True if the current user is in the given role, false otherwise. </returns>
    public bool IsInRole(string role)
    {
        return false;
    }

    /// <summary>
    /// Checks if the current user has the given permission.
    /// </summary>
    /// <param name="permission"> The permission to check. </param>
    /// <returns> True if the current user has the given permission, false otherwise. </returns>
    public bool HasPermission(string permission)
    {
        return false;
    }

    /// <summary>
    /// Checks if the current user has the given claim.
    /// </summary>
    /// <param name="claimType"> The claim type to check. </param>
    /// <param name="claimValue"> The claim value to check. </param>
    /// <returns> True if the current user has the given claim, false otherwise. </returns>
    public bool HasClaim(string claimType, string claimValue)
    {
        return false;
    }

    /// <summary>
    /// Checks if the current user has the given role.
    /// </summary>
    /// <param name="role"> The role to check. </param>
    /// <returns> True if the current user has the given role, false otherwise. </returns>
    bool HasRole(string role)
    {
        return false;
    }
}