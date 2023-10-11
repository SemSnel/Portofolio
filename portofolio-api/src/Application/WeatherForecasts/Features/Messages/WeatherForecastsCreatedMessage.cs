using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Messages;

/// <summary>
/// Will log the amount of weather forecasts in the database
/// </summary>
public sealed class WeatherForecastsCreatedMessage : IMessage
{
}

public sealed class WeatherForecastsCreatedMessageHandler : INotificationHandler<WeatherForecastsCreatedMessage>
{
    private readonly ILogger<WeatherForecastsCreatedMessageHandler> _logger;
    private readonly IWeatherForecastsRepository _weatherForecastsRepository;

    public WeatherForecastsCreatedMessageHandler(ILogger<WeatherForecastsCreatedMessageHandler> logger, IWeatherForecastsRepository weatherForecastsRepository)
    {
        _logger = logger;
        _weatherForecastsRepository = weatherForecastsRepository;
    }

    public async Task Handle(WeatherForecastsCreatedMessage notification, CancellationToken cancellationToken)
    {
        var count = await _weatherForecastsRepository.Count(cancellationToken);
        
        _logger.LogInformation("There are {Count} weather forecasts in the database", count.Value);
    }
}