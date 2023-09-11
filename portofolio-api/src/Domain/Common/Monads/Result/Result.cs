namespace SemSnel.Portofolio.Domain.Common.Monads.Result;

public readonly record struct Success;
public readonly record struct Created;
public readonly record struct Created<T>(T Value);
public readonly record struct Deleted;

public readonly record struct Updated;
public readonly record struct Updated<T>(T Value);

public static class Result
{
    public static Success Success() => default;

    public static Created Created() => default;
    
    public static Created<T> Created<T>(T value) => new(value);

    public static Deleted Deleted() => default;

    public static Updated Updated() => default;
    
    public static Updated<T> Updated<T>(T value) => new(value);
}