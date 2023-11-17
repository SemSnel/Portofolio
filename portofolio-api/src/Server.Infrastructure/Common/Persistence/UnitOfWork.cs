using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

/// <summary>
/// A unit of work for the database.
/// </summary>
/// <param name="context"> The <see cref="IAppDatabaseContext"/>. </param>
public sealed class UnitOfWork(IAppDatabaseContext context) : IUnitOfWork
{
    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success : Error.Failure("Failed to save changes");
    }
}