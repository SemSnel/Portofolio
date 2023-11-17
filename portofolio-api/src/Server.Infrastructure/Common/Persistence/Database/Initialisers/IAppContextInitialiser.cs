using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;

/// <summary>
/// Initialises the database context.
/// </summary>
public interface IAppContextInitialiser
{
    /// <summary>
    /// Initialises the database context.
    /// </summary>
    /// <param name="context"> The <see cref="IAppDatabaseContext"/>. </param>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    Task<ErrorOr<Success>> Initialise(IAppDatabaseContext context);
}