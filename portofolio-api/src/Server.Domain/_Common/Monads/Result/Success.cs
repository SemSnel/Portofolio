namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

public readonly record struct Success;

public readonly record struct Success<T>(T Value)
{
    public static implicit operator Success<T>(T value) => new (value);
    public static implicit operator T(Success<T> success) => success.Value;
}

/// <summary>
/// Extension methods for <see cref="Success{T}"/>.
/// </summary>
public static class SuccessExtensions
{
    /// <summary>
    /// Wraps a value in a Success monad.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="Success{T}"/>. </returns>
    public static Success<T> ToSuccess<T>(this T value) => new (value);
}