using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.Common.Persistence;

public interface IUnitOfWork
{
    Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default);
}