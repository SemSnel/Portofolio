using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain._Common.Monads.NoneOr;

/// <summary>
/// A monad th
/// </summary>
/// <typeparam name="T"></typeparam>
[JsonConverter(typeof(NoneOrSomeJsonConverterFactory))]
public readonly record struct NoneOr<T> : INullable
{
    private readonly T? _value;

    /// <summary>
    /// Gets a value indicating whether this instance is some.
    /// </summary>
    public bool IsSome => _value != null;

    /// <summary>
    /// Gets a value indicating whether this instance is none.
    /// </summary>
    public bool IsNone => _value == null;

    private NoneOr(T value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <exception cref="InvalidOperationException"> Cannot access the value of a None. </exception>
    public T Value
    {
        get
        {
            if (IsNone)
            {
                throw new InvalidOperationException("Cannot access the value of a None");
            }

            return _value!;
        }
    }

    /// <summary>
    /// Gets the value or null.
    /// </summary>
    public T? ValueOrNull => _value;

    /// <summary>
    /// Implicitly converts a value to a NoneOr.
    /// </summary>
    /// <returns> The NoneOr. </returns>
    public static NoneOr<T> None() => new ();

    /// <summary>
    /// Implicitly converts a value to a NoneOr.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> The NoneOr. </returns>
    /// <exception cref="ArgumentNullException"> value is null. </exception>
    public static NoneOr<T> Some(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new (value);
    }

    public static implicit operator NoneOr<T>(T value) => new (value);
    public static implicit operator T?(NoneOr<T> noneOr) => noneOr._value;

    /// <inheritdoc/>
    public override string ToString() => IsSome ? $"Some({_value})" : "None";

    /// <inheritdoc/>
    public override int GetHashCode() => IsSome ? _value!.GetHashCode() : 0;

    /// <summary>
    /// Executes the specified action if the value is some.
    /// </summary>
    /// <param name="some"> The action. </param>
    /// <param name="none"> The none action. </param>
    public void Match(Action<T> some, Action none)
    {
        if (IsSome)
        {
            some(_value!);
        }
        else
        {
            none();
        }
    }

    /// <summary>
    /// Executes the specified action if the value is some.
    /// </summary>
    /// <param name="some"> The action. </param>
    /// <param name="none"> The none action. </param>
    /// <typeparam name="TResult"> The type of the result. </typeparam>
    /// <returns> The result. </returns>
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        return IsSome ? some(_value!) : none();
    }

    /// <summary>
    /// Executes the specified action if the value is some.
    /// </summary>
    /// <param name="some"> The action. </param>
    /// <param name="none"> The none action. </param>
    /// <typeparam name="TResult"> The type of the result. </typeparam>
    /// <returns> The result. </returns>
    public TResult Match<TResult>(Func<T, TResult> some, TResult none)
    {
        return IsSome ? some(_value!) : none;
    }

    /// <inheritdoc/>
    public bool IsNull => !IsSome;
}

/// <summary>
/// Extension methods for <see cref="NoneOr{T}"/>.
/// </summary>
public static class NoneOr
{
    /// <summary>
    /// Maps the <see cref="NoneOr{T}"/> to a <see cref="NoneOr{T}"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="NoneOr{T}"/>. </returns>
    public static NoneOr<T> Some<T>(T value) => value;

    /// <summary>
    /// Maps the <see cref="NoneOr{T}"/> to a <see cref="NoneOr{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="NoneOr{T}"/>. </returns>
    public static NoneOr<T> None<T>() => default;

    /// <summary>
    /// Maps the <see cref="NoneOr{T}"/> to a <see cref="NoneOr{T}"/>.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="NoneOr{T}"/>. </returns>
    public static NoneOr<T> ToNoneOrSome<T>(this T? value) => value!;

    /// <summary>
    /// Creates a NoneOr from a nullable value.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The <see cref="NoneOr{T}"/>. </returns>
    public static NoneOr<T> Create<T>(T? value)
    {
        return value == null ? None<T>() : Some(value);
    }
}