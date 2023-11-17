using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// A pipeline behaviour that wraps the request in a transaction.
/// </summary>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public sealed class UnitOfWorkBehaviour<TRequest, TResponse>
    (IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.IsError)
        {
            return response;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}