using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Update;

public record UpdateWeatherForecastsCommand(
        Guid Id,
        DateTime Date,
        int TemperatureC,
        string Summary
    )
    : IRequest<ErrorOr<Updated<Guid>>>;

public class UpdateWeatherForecastsCommandHandler : IRequestHandler<UpdateWeatherForecastsCommand, ErrorOr<Updated<Guid>>>
{
    private readonly IWeatherForecastsRepository _repository;

    public UpdateWeatherForecastsCommandHandler(IWeatherForecastsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Updated<Guid>>> Handle(
        UpdateWeatherForecastsCommand request,
        CancellationToken cancellationToken)
    {
        var errorOr = await _repository.GetById(request.Id, cancellationToken);

        return await errorOr.MatchAsync(
            forecast => OnFound(request, forecast, cancellationToken),
            (errors) => OnError(request));
}

    private async Task<ErrorOr<Updated<Guid>>> OnFound(UpdateWeatherForecastsCommand command, WeatherForecast forecast, CancellationToken cancellationToken)
    {
        forecast.Update(command.Date, command.TemperatureC, command.Summary);

        return await _repository.Update(forecast, cancellationToken);
    }

    private Task<ErrorOr<Updated<Guid>>> OnError(UpdateWeatherForecastsCommand request)
    {
        var error = ErrorOr<Updated<Guid>>.From(Error.NotFound($"Weather forecast with id {request.Id} was not found"));

        return Task.FromResult(error);
    }
}
