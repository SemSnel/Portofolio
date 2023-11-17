using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Persistence;

/// <summary>
/// Repository for <see cref="WeatherForecast"/>s.
/// </summary>
public sealed class WeatherForecastsRepository : Repository<WeatherForecast, Guid>, IWeatherForecastsRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastsRepository"/> class.
    /// </summary>
    /// <param name="context"> The <see cref="IAppDatabaseContext"/>. </param>
    /// <param name="mapper"> The <see cref="IMapper"/>. </param>
    public WeatherForecastsRepository(IAppDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }
}