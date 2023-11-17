using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.NoneOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

/// <summary>
/// An idempotency service.
/// </summary>
public interface IIdempotencyService
{
    /// <summary>
    /// Checks if a request with the given id exists.
    /// </summary>
    /// <param name="id"> The id. </param>
    /// <returns> The <see cref="ErrorOr{T}"/>. </returns>
    Task<ErrorOr<bool>> Exists(Guid id);

    /// <summary>
    /// Saves a request.
    /// </summary>
    /// <param name="request"> The request. </param>
    /// <returns> The <see cref="ErrorOr{T}"/>. </returns>
    Task<ErrorOr<Created>> SaveRequest(IdempotentRequest request);

    /// <summary>
    /// Deletes a request.
    /// </summary>
    /// <param name="id"> The id. </param>
    /// <returns> The <see cref="ErrorOr{T}"/>. </returns>
    Task<ErrorOr<Success>> DeleteRequest(Guid id);

    /// <summary>
    /// Gets a request.
    /// </summary>
    /// <param name="idempotencyKey"> The idempotency key. </param>
    /// <returns> The <see cref="ErrorOr{T}"/>. </returns>
    Task<ErrorOr<NoneOr<IdempotentRequest>>> GetRequest(Guid idempotencyKey);
}