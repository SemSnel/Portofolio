using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

/// <summary>
/// A seeder for the <see cref="AppDbContext"/>.
/// </summary>
public interface IAppDbContextSeeder
{
    /// <summary>
    /// Seeds the database.
    /// </summary>
    /// <param name="context"> The <see cref="IAppDatabaseContext"/>. </param>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    Task<ErrorOr<Success>> Seed(IAppDatabaseContext context);
}