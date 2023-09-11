using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

public interface IAppDbContextSeeder
{
    Task<ErrorOr<Success>> Seed(IAppDatabaseContext context);
}