using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Domain.Identity;

/// <summary>
/// A permission.
/// </summary>
public readonly record struct Permission(string Name)
{
    private const string EmptyName = "";

    /// <summary>
    /// Creates a new permission.
    /// </summary>
    /// <param name="name"> The name. </param>
    /// <returns> The <see cref="ErrorOr{T}"/>. </returns>
    public static ErrorOr<Permission> Create(string name)
    {
        return name switch
        {
            null => Error.Validation("The name cannot be null."),
            EmptyName => Error.Validation("The name cannot be empty."),
            _ => new Permission(name)
        };
    }

    public static implicit operator Permission(string name) => Create(name).Value;
    public static implicit operator string(Permission permission) => permission.Name;
}