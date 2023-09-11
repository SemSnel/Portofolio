namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public class Either<TLeft, TRight> : IEither<TLeft, TRight>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    
    public bool IsLeft { get; }
    public bool IsRight => !IsLeft;
    
    private Either(TLeft left)
    {
        _left = left;
        IsLeft = true;
    }
    
    private Either(TRight right)
    {
        _right = right;
        IsLeft = false;
    }
    
    
    public void Match(Action<TLeft> left, Action<TRight> right)
    {
        if (IsLeft)
        {
            left(_left!);
            return;
        }
        
        right(_right!);
    }

    public TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return IsLeft ? left(_left!) : right(_right!);
    }

    public TResult Match<TResult>(Func<TLeft, TResult> left, TResult right)
    {
        return IsLeft ? left(_left!) : right;
    }
    
    public static implicit operator Either<TLeft, TRight>(TLeft left) => new(left);
    public static implicit operator Either<TLeft, TRight>(TRight right) => new(right);
}