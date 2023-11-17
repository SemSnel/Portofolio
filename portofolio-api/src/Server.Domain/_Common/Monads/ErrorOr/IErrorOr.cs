namespace SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

/// <summary>
/// The error or.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public interface IErrorOr<TValue> : IErrorOr
{
    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will be empty.
    /// </summary>
    List<Error> ErrorsOrEmptyList { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    TValue Value { get; }

    /// <summary>
    /// Gets the first error.
    /// </summary>
    Error FirstError { get; }
}

/// <summary>
/// Base interface for error or.
/// </summary>
public interface IErrorOr
{
    /// <summary>
    /// Gets the list of errors.
    /// </summary>
    public List<Error>? Errors { get; }

    /// <summary>
    /// Gets a value indicating whether the state is error.
    /// </summary>
    public bool IsError { get; }
}