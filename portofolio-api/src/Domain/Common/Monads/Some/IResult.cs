namespace SemSnel.Portofolio.Domain.Common.Monads.Some;

/// <summary>
/// A type that represents either a value of type T or nothing at all.
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INoneOr<T>
{
    /// <summary>
    /// A boolean value that indicates whether the value is some.
    /// </summary>
    public bool IsSome { get; }
    /// <summary>
    /// A boolean value that indicates whether the value is none.
    /// </summary>
    public bool IsNone { get; }

    /// <summary>
    /// Invokes the given action if the value is some, otherwise does the action when the value is none.
    /// </summary>
    /// <param name="some"> The action to invoke when the value is some. </param>
    /// <param name="none"> The action to invoke when the value is none. </param>
    public void Match(Action<T> some, Action none);
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);
    public TResult Match<TResult>(Func<T, TResult> some, TResult none);
}

/// <summary>
/// A result is a type that represents either success or failure.
/// It is a type that is used to represent the possible outcome of a computation.
/// By convention, the result type has two subtypes: success and failure.
/// </summary>
/// <typeparam name="TSuccess">
///     The type of the success value.
/// </typeparam>
/// <typeparam name="TFailure">
///     The type of the failure value.
/// </typeparam>
public interface IResult<TSuccess, TFailure>
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }

    public void Match(Action<TSuccess> onSuccess, Action<TFailure> onFailure);
    public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFailure, TResult> onFailure);
    public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, TResult onFailure);
}