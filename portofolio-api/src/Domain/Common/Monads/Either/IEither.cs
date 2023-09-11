namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public interface IEither<TLeft, TRight>
{
    public bool IsLeft { get; }
    public bool IsRight { get; }
    public void Match(Action<TLeft> left, Action<TRight> right);
    public TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right);
    public TResult Match<TResult>(Func<TLeft, TResult> left, TResult right);
}