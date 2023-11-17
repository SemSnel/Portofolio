using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Events;
using SemSnel.Portofolio.Server.Application.Common.Messages;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Events;

public record WeatherForecastCreatedNotificationHandler : INotificationHandler<DomainMessageNotification<WeatherForecastCreatedDomainEvent>>
{
    private readonly ILogger<WeatherForecastCreatedNotificationHandler> _logger;

    public WeatherForecastCreatedNotificationHandler(ILogger<WeatherForecastCreatedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainMessageNotification<WeatherForecastCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Weather forecast created: {WeatherForecast}", notification.Event);

        return Task.CompletedTask;
    }
}