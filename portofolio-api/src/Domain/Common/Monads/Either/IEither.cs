namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public interface IEither<TLeft, TRight>
{
    public bool IsLeft { get; }
    public bool IsRight { get; }
    public TLeft Left { get; }
    public TRight Right { get; }
}