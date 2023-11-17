namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

public readonly record struct Updated<T>(T Value)
{
    public static implicit operator Updated<T>(T value) => new (value);
    public static implicit operator T(Updated<T> updated) => updated.Value;
}

public readonly record struct Updated;

/// <summary>
/// Extension methods for <see cref="Updated{T}"/>.
/// </summary>
public static class UpdatedExtensions
{
    /// <summary>
    /// Maps the <see cref="Updated{T}"/> to a <see cref="Updated"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="Updated"/>. </returns>
    public static Updated<T> ToUpdated<T>(this T value) => value;
}