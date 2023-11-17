namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

/// <summary>
/// A static class that contains the <see cref="Success"/>, <see cref="Created"/>, <see cref="Deleted"/> and <see cref="Updated"/> types.
/// </summary>
public static class Result
{
    /// <summary>
    /// Gets the <see cref="Success"/> type.
    /// </summary>
    public static Success Success => default;

    /// <summary>
    /// Gets the <see cref="Created"/> type.
    /// </summary>
    public static Created Created => default;

    /// <summary>
    /// Gets the <see cref="Deleted"/> type.
    /// </summary>
    public static Deleted Deleted => default;

    /// <summary>
    /// Gets the <see cref="Updated"/> type.
    /// </summary>
    public static Updated Updated => default;
}