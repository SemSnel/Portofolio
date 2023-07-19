namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public class Either<TLeft, TRight> : IEither<TLeft, TRight>
{
    public bool IsLeft { get; }
    public bool IsRight { get; }
    public TLeft Left { get; }
    public TRight Right { get; }

    public Either(TLeft left)
    {
        IsLeft = true;
        IsRight = false;
        Left = left;
        Right = default!;
    }

    public Either(TRight right)
    {
        IsLeft = false;
        IsRight = true;
        Left = default!;
        Right = right;
    }
}