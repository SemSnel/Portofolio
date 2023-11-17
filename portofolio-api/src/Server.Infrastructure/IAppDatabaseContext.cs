using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;

namespace SemSnel.Portofolio.Infrastructure;

/// <summary>
/// The database context for the application.
/// </summary>
public interface IAppDatabaseContext :
    IIdempotencyDatabaseContext,
    IMessagesDatabaseContext
{
    /// <summary>
    /// Gets the weather forecasts table.
    /// </summary>
    DbSet<WeatherForecast> WeatherForecasts { get; }
}