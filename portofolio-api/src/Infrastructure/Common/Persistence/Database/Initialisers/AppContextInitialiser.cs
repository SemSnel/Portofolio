using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;

public sealed class AppContextInitialiser : IAppContextInitialiser
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<AppContextInitialiser> _logger;
    private readonly IAppDbContextSeeder _seeder;

    public AppContextInitialiser(IDateTimeProvider dateTimeProvider, ILogger<AppContextInitialiser> logger, IAppDbContextSeeder seeder)
    {
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        _seeder = seeder;
    }

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
        
        var result = await _seeder.Seed(context);

        return result;
    }
}