namespace SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

/// <summary>
/// Extension methods for <see cref="ErrorOr{TValue}"/>.
/// </summary>
public static partial class ErrorOr
{
    /// <summary>
    /// Maps the <see cref="ErrorOr{TValue}"/> to a <see cref="ErrorOr"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="TValue"> The type. </typeparam>
    /// <returns> The <see cref="ErrorOr"/>. </returns>
    public static ErrorOr<TValue> From<TValue>(TValue value)
    {
        return value;
    }

    /// <summary>
    /// Maps the <see cref="Error"/> to a <see cref="ErrorOr{TValue}"/>.
    /// </summary>
    /// <param name="error"> The <see cref="Error"/>. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="ErrorOr{TValue}"/>. </returns>
    public static ErrorOr<T> From<T>(Error error)
    {
        return error;
    }
}
