namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

public static class ErrorOrFactory
{
    public static ErrorOr<TValue> From<TValue>(TValue value)
    {
        return value;
    }
}
