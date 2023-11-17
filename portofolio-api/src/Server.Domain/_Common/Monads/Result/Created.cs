namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

public readonly record struct Created<T>(T Value)
{
    public static implicit operator Created<T>(T value) => new (value);
    public static implicit operator T(Created<T> created) => created.Value;
}

public readonly record struct Created;