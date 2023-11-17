using FluentValidation;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Validations;

/// <summary>
/// A validation behaviour that validates the request.
/// </summary>
/// <param name="validators"> The validators. </param>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public class ValidationBehaviour<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>>? validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators is null)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationTasks = validators
            .Select(async validator => await validator.ValidateAsync(context, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var isValid = true;

        foreach (var validationResult in validationResults)
        {
            if (validationResult.IsValid)
            {
                continue;
            }

            isValid = false;

            break;
        }

        // check if all validation results are valid
        if (isValid)
        {
            return await next();
        }

        var errors = validationResults
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        return (dynamic)errors;
    }
}