namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

/// <summary>
/// Extension methods for <see cref="Deleted{T}"/>.
/// </summary>
public static class DeletedExtensions
{
    /// <summary>
    /// Maps the <see cref="Deleted{T}"/> to a <see cref="Deleted"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="Deleted"/>. </returns>
    public static Deleted<T> ToDeleted<T>(this T value) => value;
}