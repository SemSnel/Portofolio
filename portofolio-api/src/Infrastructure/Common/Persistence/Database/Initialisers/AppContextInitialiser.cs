using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;

public sealed class AppContextInitialiser : IAppContextInitialiser
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppContextInitialiser(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Success>> Initialise(IAppDatabaseContext context)
    {
        var anyWeatherForecasts = await context
            .WeatherForecasts
            .AnyAsync();
        
        if (anyWeatherForecasts)
            return Result.Success();
        
        var now = _dateTimeProvider.Now();

        var forecasts = Enumerable.Range(1, 50).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Summary"
        });
        
        context
            .WeatherForecasts
            .AddRangeAsync(forecasts);
        
        // save changes
        await context
            .SaveChangesAsync();

        return Result.Success();
    }
}