namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

/// <summary>
/// Error types.
/// </summary>
public enum ErrorType
{
    Failure,
    Unexpected,
    Validation,
    Forbidden,
    Unauthorized,
    Conflict,
    NotFound,
    NoError
}
