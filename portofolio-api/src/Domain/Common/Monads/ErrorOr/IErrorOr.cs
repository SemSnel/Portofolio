namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

public interface IErrorOr<TValue> : IErrorOr
{
    /// <summary>
    /// Gets a value indicating whether the state is error.
    /// </summary>
    bool IsError { get; }

    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will contain a single error representing the state.
    /// </summary>
    List<Error> Errors { get; }

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

    void Switch(Action<TValue> onValue, Action<List<Error>> onError);
    Task SwitchAsync(Func<TValue, Task> onValue, Func<List<Error>, Task> onError);
    void SwitchFirst(Action<TValue> onValue, Action<Error> onFirstError);
    Task SwitchFirstAsync(Func<TValue, Task> onValue, Func<Error, Task> onFirstError);
    TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError);
    Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onValue, Func<List<Error>, Task<TResult>> onError);
    TResult MatchFirst<TResult>(Func<TValue, TResult> onValue, Func<Error, TResult> onFirstError);
    Task<TResult> MatchFirstAsync<TResult>(Func<TValue, Task<TResult>> onValue, Func<Error, Task<TResult>> onFirstError);

    bool Equals(object? other);
    int GetHashCode();
    string ToString();
}

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