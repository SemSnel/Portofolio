using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure;

/// <summary>
/// A database context for the application.
/// </summary>
/// <param name="options"> The <see cref="DbContextOptions{TContext}"/>. </param>
/// <param name="interceptors"> The <see cref="IEnumerable{T}"/> of <see cref="IInterceptor"/>. </param>
public sealed class AppDatabaseContext(
    DbContextOptions<AppDatabaseContext> options,
    IEnumerable<IInterceptor> interceptors)
    : BaseDatabaseContext(options, interceptors),
        IAppDatabaseContext
{
    /// <inheritdoc/>
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
 }