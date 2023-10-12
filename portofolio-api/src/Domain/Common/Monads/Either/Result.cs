namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public class Result<TLeft, TRight> : IResult<TLeft, TRight>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    
    public bool IsSucces { get; }
    public bool IsError => !IsSucces;
    
    private Result(TLeft left)
    {
        _left = left;
        IsSucces = true;
    }
    
    private Result(TRight right)
    {
        _right = right;
        IsSucces = false;
    }
    
    
    public void Match(Action<TLeft> onSucces, Action<TRight> onError)
    {
        if (IsSucces)
        {
            onSucces(_left!);
            return;
        }
        
        onError(_right!);
    }

    public TResult Match<TResult>(Func<TLeft, TResult> onSucces, Func<TRight, TResult> onError)
    {
        return IsSucces ? onSucces(_left!) : onError(_right!);
    }

    public TResult Match<TResult>(Func<TLeft, TResult> onSucces, TResult result)
    {
        return IsSucces ? onSucces(_left!) : result;
    }
    
    public static implicit operator Result<TLeft, TRight>(TLeft left) => new(left);
    public static implicit operator Result<TLeft, TRight>(TRight right) => new(right);
}