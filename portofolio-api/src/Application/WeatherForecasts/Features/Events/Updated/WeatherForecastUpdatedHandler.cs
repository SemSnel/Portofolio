using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Events.Updated;

public class WeatherForecastUpdatedHandler : INotificationHandler<WeatherForecastUpdatedEvent>
{
    public Task Handle(WeatherForecastUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}