using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Repositories;

public interface IWeatherForecastsRepository : 
    IReadRepository<WeatherForecast, Guid>,
    IWriteRepository<WeatherForecast, Guid>,
    ISearchableReadRepository<WeatherForecast, Guid>
{
}