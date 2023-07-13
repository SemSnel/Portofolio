namespace SemSnel.Portofolio.Domain.Common.Monads.Some;

public interface ISome<T>
{
    public bool IsSome { get; }
    public bool IsNone { get; }
    public T Value { get; }
}

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
}