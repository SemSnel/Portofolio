namespace SemSnel.Portofolio.Domain.Common.Monads.Some;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class NoneOr<T> : INoneOr<T>
{
    private readonly T _value;
    public bool IsSome { get; }
    public bool IsNone => !IsSome;
    
    private NoneOr()
    {
        _value = default!;
        IsSome = false;
    }
    
    private NoneOr(T value)
    {
        _value = value;
        IsSome = true;
    }
    
    public static NoneOr<T> None() => new();
    
    public static implicit operator NoneOr<T>(T value) => new(value);
    
    public static implicit operator T(NoneOr<T> noneOr) => noneOr._value;
    
    public override string ToString() => IsSome ? $"Some({_value})" : "None";
    
    public override int GetHashCode() => IsSome ? _value!.GetHashCode() : 0;

    public override bool Equals(object? obj) => obj is NoneOr<T> some && Equals(some);
    
    // match
    public void Match(Action<T> some, Action none)
    {
        if (IsSome)
        {
            some(_value);
        }
        else
        {
            none();
        }
    }
    
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        return IsSome ? some(_value) : none();
    }
    
    public TResult Match<TResult>(Func<T, TResult> some, TResult none)
    {
        return IsSome ? some(_value) : none;
    }
}