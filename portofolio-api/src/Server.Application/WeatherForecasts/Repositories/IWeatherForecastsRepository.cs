using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

/// <summary>
/// An interface that represents the weather forecasts repository.
/// </summary>
public interface IWeatherForecastsRepository :
    IReadRepository<WeatherForecast, Guid>,
    IWriteRepository<WeatherForecast, Guid>,
    ISearchableReadRepository<WeatherForecast, Guid>;