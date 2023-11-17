namespace SemSnel.Portofolio.Domain._Common.Monads.Result;

public readonly record struct Deleted<T>(T Value)
{
    public static implicit operator Deleted<T>(T value) => new (value);
    public static implicit operator T(Deleted<T> deleted) => deleted.Value;
}

public readonly record struct Deleted;