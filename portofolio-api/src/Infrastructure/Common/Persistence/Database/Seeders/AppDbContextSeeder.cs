using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts.Fakers;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

public sealed class AppDbContextSeeder : IAppDbContextSeeder
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IWeatherForecastFaker _weatherForecastFaker;

    public AppDbContextSeeder(IDateTimeProvider dateTimeProvider, IWeatherForecastFaker weatherForecastFaker)
    {
        _dateTimeProvider = dateTimeProvider;
        _weatherForecastFaker = weatherForecastFaker;
    }

    public async Task<ErrorOr<Success>> Seed(IAppDatabaseContext context)
    {
        var anyWeatherForecasts = await context
            .WeatherForecasts
            .AnyAsync();

        if (anyWeatherForecasts)
        {
            return Result.Success();
        }
        
        var now = _dateTimeProvider.Now();

        var forecasts = _weatherForecastFaker
            .Generate(100, "default")
            .Select(x => new WeatherForecast
            {
                Date = x.Date,
                TemperatureC = x.TemperatureC,
                Summary = x.Summary,
                CreatedOn = now,
                LastModifiedOn = now
            })
            .ToList();
        
        await context
            .WeatherForecasts
            .AddRangeAsync(forecasts);
        
        // save changes
        await context
            .SaveChangesAsync();
        
        return Result.Success();
    }
}