using System.Text.Json.Serialization;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

/// <summary>
/// A discriminated union of errors or a value.
/// </summary>
[JsonConverter(typeof(ErrorOrJsonConverterFactory))]
public readonly record struct ErrorOr<TValue> : IErrorOr
{
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors = null;

    private static readonly Error NoFirstError = Error.Unexpected(
        code: "ErrorOr.NoFirstError",
        description: "First error cannot be retrieved from a successful ErrorOr.");

    private static readonly Error NoErrors = Error.Unexpected(
        code: "ErrorOr.NoErrors",
        description: "Error list cannot be retrieved from a successful ErrorOr.");

    /// <summary>
    /// Gets a value indicating whether the state is error.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will contain a single error representing the state.
    /// </summary>
    public List<Error> Errors => IsError ? _errors! : new List<Error> { NoErrors };

    /// <summary>
    /// Gets the list of errors. If the state is not error, the list will be empty.
    /// </summary>
    public List<Error> ErrorsOrEmptyList => IsError ? _errors! : new ();

    /// <summary>
    /// Creates an <see cref="ErrorOr{TValue}"/> from a value.
    /// </summary>
    /// <param name="errors"> The list of errors. </param>
    /// <returns> The <see cref="ErrorOr{TValue}"/>. </returns>
    public static ErrorOr<TValue> From(List<Error> errors)
    {
        return errors;
    }

    /// <summary>
    /// Creates an <see cref="ErrorOr{TValue}"/> from a error.
    /// </summary>
    /// <param name="error"> The error. </param>
    /// <returns> The <see cref="ErrorOr{TValue}"/>. </returns>
    public static ErrorOr<TValue> From(Error error)
    {
        return error;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public TValue Value => _value!;

    /// <summary>
    /// Gets the first error.
    /// </summary>
    public Error FirstError
    {
        get
        {
            if (!IsError)
            {
                return NoFirstError;
            }

            return _errors![0];
        }
    }

    private ErrorOr(Error error)
    {
        _errors = new List<Error> { error };
        IsError = true;
    }

    private ErrorOr(List<Error> errors)
    {
        _errors = errors;
        IsError = true;
    }

    private ErrorOr(TValue value)
    {
        _value = value;
        IsError = false;
    }

    public static implicit operator ErrorOr<TValue>(TValue value)
    {
        return new ErrorOr<TValue>(value);
    }

    public static implicit operator ErrorOr<TValue>(Error error)
    {
        return new ErrorOr<TValue>(error);
    }

    public static implicit operator ErrorOr<TValue>(List<Error> errors)
    {
        return new ErrorOr<TValue>(errors);
    }

    public static implicit operator ErrorOr<TValue>(Error[] errors)
    {
        return new ErrorOr<TValue>(errors.ToList());
    }

    /// <summary>
    /// Matches the state of the <see cref="ErrorOr{TValue}"/> and executes the corresponding action.
    /// </summary>
    /// <param name="onSuccess"> The success action. </param>
    /// <param name="onError"> The error action. </param>
    /// <typeparam name="TResult"> The type of the result. </typeparam>
    /// <returns> The result. </returns>
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<List<Error>, TResult> onError)
    {
        return IsError ? onError(Errors) : onSuccess(Value);
    }

    /// <summary>
    /// Matches the state of the <see cref="ErrorOr{TValue}"/> and executes the corresponding action.
    /// </summary>
    /// <typeparam name="TResult"> The type of the result. </typeparam>
    /// <param name="onSuccess"> The success action. </param>
    /// <param name="onError"> The error action. </param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onSuccess, Func<List<Error>, Task<TResult>> onError)
    {
        if (IsError)
        {
            return await onError(Errors).ConfigureAwait(false);
        }

        return await onSuccess(Value).ConfigureAwait(false);
    }

    /// <summary>
    /// Matches the state of the <see cref="ErrorOr{TValue}"/> and executes the corresponding action.
    /// </summary>
    /// <param name="onSuccess"> The success action. </param>
    /// <param name="onFirstError"> The first error action. </param>
    /// <typeparam name="TResult"> The type of the result. </typeparam>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public TResult MatchFirst<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFirstError)
    {
        return IsError ? onFirstError(FirstError) : onSuccess(Value);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return IsError ? $"ErrorOr({Errors})" : $"ErrorOr({Value})";
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return IsError ? Errors.GetHashCode() : Value!.GetHashCode();
    }

    public static implicit operator ErrorOr<Success>(ErrorOr<TValue> errorOr)
    {
        return errorOr.IsError ? errorOr.Errors : default(Success);
    }
}