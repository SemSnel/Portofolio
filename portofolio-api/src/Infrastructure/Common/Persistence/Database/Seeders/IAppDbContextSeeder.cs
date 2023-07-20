using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

public interface IAppDbContextSeeder
{
    Task<ErrorOr<Success>> Seed(IAppDatabaseContext context);
}

public sealed class AppDbContextSeeder : IAppDbContextSeeder
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppDbContextSeeder(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
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

        var forecasts = Enumerable.Range(1, 50).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Summary"
        });
        
        await context
            .WeatherForecasts
            .AddRangeAsync(forecasts);
        
        // save changes
        await context
            .SaveChangesAsync();
        
        return Result.Success();
    }
}