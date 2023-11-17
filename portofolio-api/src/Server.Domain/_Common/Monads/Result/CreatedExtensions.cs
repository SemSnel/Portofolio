namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

/// <summary>
/// Extension methods for <see cref="Created{T}"/>.
/// </summary>
public static class CreatedExtensions
{
    /// <summary>
    /// Maps the <see cref="Created{T}"/> to a <see cref="Created"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="Created"/>. </returns>
    public static Created<T> ToCreated<T>(this T value) => value;
}