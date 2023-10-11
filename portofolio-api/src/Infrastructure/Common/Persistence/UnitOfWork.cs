using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IAppDatabaseContext _context;

    public UnitOfWork(IAppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.Failure("Failed to save changes");
    }
}