using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// A unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The result. </returns>
    Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default);
}