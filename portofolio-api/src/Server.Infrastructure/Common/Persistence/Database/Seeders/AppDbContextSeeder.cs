using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Fakers;
using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

/// <summary>
/// A seeder for the <see cref="AppDbContext"/>.
/// </summary>
public sealed class AppDbContextSeeder : IAppDbContextSeeder
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IWeatherForecastFaker _weatherForecastFaker;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContextSeeder"/> class.
    /// </summary>
    /// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
    /// <param name="weatherForecastFaker"> The <see cref="IWeatherForecastFaker"/>. </param>
    public AppDbContextSeeder(IDateTimeProvider dateTimeProvider, IWeatherForecastFaker weatherForecastFaker)
    {
        _dateTimeProvider = dateTimeProvider;
        _weatherForecastFaker = weatherForecastFaker;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Seed(IAppDatabaseContext context)
    {
        var result = await SeedForecasts(context);

        return result.IsError ? result : Result.Success;
    }

    private async Task<ErrorOr<Success>> SeedForecasts(IAppDatabaseContext context)
    {
        var anyWeatherForecasts = await context
            .WeatherForecasts
            .AnyAsync();

        if (anyWeatherForecasts)
        {
            return Result.Success;
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
                LastModifiedOn = now,
            })
            .ToList();

        await context
            .WeatherForecasts
            .AddRangeAsync(forecasts);

        await context
            .SaveChangesAsync();

        return Result.Success;
    }
}