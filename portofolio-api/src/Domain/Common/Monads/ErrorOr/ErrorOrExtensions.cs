namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

public static class ErrorOrExtensions
{
    public static ErrorOr<TValue> From<TValue>(TValue value)
    {
        return value;
    }
    
    public static ErrorOr<TValue> From<TValue>(Exception exception)
    {
        return Error.Unexpected(exception.Message);
    }
    
    public static ErrorOr<TValue> From<TValue>(Error error)
    {
        return error;
    }
    
    public static ErrorOr<TValue> From<TValue>(List<Error> errors)
    {
        return errors;
    }
}