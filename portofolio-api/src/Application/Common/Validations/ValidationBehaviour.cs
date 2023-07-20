using FluentValidation;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Validations;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>>? validators)
    {
        _validators = validators;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationTasks = _validators
            .Select(async validator => await validator.ValidateAsync(context, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var isValid = true;
        
        foreach (var validationResult in validationResults)
        {
            if (validationResult.IsValid) 
                continue;
            
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