using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public record UpdateWeatherForecastsCommand : IRequest<ErrorOr<Updated<Guid>>>
{
    public Guid Id { get; init; }
    public DateOnly Date { get; init; }
    public int TemperatureC { get; init; }
    public string? Summary { get; init; }
}

public class UpdateWeatherForecastsCommandHandler : IRequestHandler<UpdateWeatherForecastsCommand, ErrorOr<Updated<Guid>>>
{
    private readonly IWeatherForecastsRepository _repository;

    public UpdateWeatherForecastsCommandHandler(IWeatherForecastsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Updated<Guid>>> Handle(UpdateWeatherForecastsCommand request,
        CancellationToken cancellationToken)
    {
        var errorOr = await _repository.GetById(request.Id, cancellationToken);

        return await errorOr.MatchAsync(
            forecast => OnFound(request, forecast, cancellationToken),
            (errors) => OnError(request));
}

    private async Task<ErrorOr<Updated<Guid>>> OnFound(UpdateWeatherForecastsCommand command,WeatherForecast forecast, CancellationToken cancellationToken)
    {
        forecast.Update(command.Date, command.TemperatureC, command.Summary);
        
        return  _repository.Update(forecast, cancellationToken);
    }
    
    private static Task<ErrorOr<Updated<Guid>>> OnError(UpdateWeatherForecastsCommand request)
    {
        var error = ErrorOr<Updated<Guid>>.From(Error.NotFound($"Weather forecast with id {request.Id} was not found"));

        return Task.FromResult(error);
    }
}
