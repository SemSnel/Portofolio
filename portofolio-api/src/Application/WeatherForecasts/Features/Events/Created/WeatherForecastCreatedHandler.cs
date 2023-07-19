using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Events.Created;

public sealed class WeatherForecastCreatedHandler : INotificationHandler<WeatherForecastCreated>
{
    private readonly ILogger<WeatherForecastCreatedHandler> _logger;

    public WeatherForecastCreatedHandler(ILogger<WeatherForecastCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WeatherForecastCreated notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Weather forecast created: {Id}", notification.Id);

        return Task.CompletedTask;
    }
}