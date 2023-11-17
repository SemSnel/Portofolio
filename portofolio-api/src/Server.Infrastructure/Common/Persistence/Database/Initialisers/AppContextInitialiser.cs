using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;

/// <summary>
/// An initialiser for the <see cref="AppDbContext"/>.
/// </summary>
public sealed class AppContextInitialiser(IAppDbContextSeeder seeder)
    : IAppContextInitialiser
{
    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Initialise(IAppDatabaseContext context)
    {
        try
        {
            await context.MigrateAsync();
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }

        var result = await seeder.Seed(context);

        return result;
    }
}