namespace SemSnel.Portofolio.Domain.Common.Monads.Some;

public interface ISome<T>
{
    public bool IsSome { get; }
    public bool IsNone { get; }
    public T Value { get; }
    
    public void Match(Action<T> some, Action none);
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);
    public TResult Match<TResult>(Func<T, TResult> some, TResult none);
}