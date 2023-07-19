namespace SemSnel.Portofolio.Domain.Common.Monads.Some;

public sealed class Some<T> : ISome<T>
{
    private readonly T? _value;
    
    public bool IsSome { get; }
    public bool IsNone => !IsSome;
    public T Value => _value ?? throw new InvalidOperationException("Value is null");
    
    public Some()
    {
        _value = default;
        IsSome = false;
    }
    
    public Some(T value)
    {
        _value = value;
        IsSome = true;
    }
    
    public static Some<T> None() => new();
    
    public static implicit operator Some<T>(T value) => new(value);
    
    public static implicit operator T(Some<T> some) => some.Value;
    
    public override string ToString() => IsSome ? $"Some({Value})" : "None";
    
    public override int GetHashCode() => IsSome ? Value.GetHashCode() : 0;

    public override bool Equals(object? obj) => obj is Some<T> some && Equals(some);
    
    // match
    public void Match(Action<T> some, Action none)
    {
        if (IsSome)
        {
            some(Value);
        }
        else
        {
            none();
        }
    }
    
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        return IsSome ? some(Value) : none();
    }
    
    public TResult Match<TResult>(Func<T, TResult> some, TResult none)
    {
        return IsSome ? some(Value) : none;
    }
}