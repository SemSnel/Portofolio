namespace SemSnel.Portofolio.Domain.Common.Monads.Either;

public interface IResult<TSucces, TError>
{
    public bool IsSucces { get; }
    public bool IsError { get; }
    public void Match(Action<TSucces> onSucces, Action<TError> onError);
    public TResult Match<TResult>(Func<TSucces, TResult> onSucces, Func<TError, TResult> onError);
    public TResult Match<TResult>(Func<TSucces, TResult> onSucces, TResult result);
}