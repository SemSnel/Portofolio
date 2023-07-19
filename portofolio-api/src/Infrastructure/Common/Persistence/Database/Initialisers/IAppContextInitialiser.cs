using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;

public interface IAppContextInitialiser
{
    Task<ErrorOr<Success>> Initialise(IAppDatabaseContext context);
}