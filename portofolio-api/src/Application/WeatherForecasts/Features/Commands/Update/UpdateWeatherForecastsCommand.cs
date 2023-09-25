using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public class UpdateWeatherForecastsCommand : IRequest<ErrorOr<Updated<Guid>>>
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


    public async Task<ErrorOr<Updated<Guid>>> Handle(UpdateWeatherForecastsCommand request, CancellationToken cancellationToken)
    {
        var errorOr = await _repository.GetById(request.Id, cancellationToken);
        
        /*if (errorOr.IsError)
        {
            return Error.NotFound($"Weather forecast with id {request.Id} was not found");
        }
        
        var weatherForecast = errorOr.Value;
        
        var update = weatherForecast.Update(request.Date, request.TemperatureC, request.Summary);
        
        var result = await _repository.Update(update, cancellationToken);*/
        /* make code with match */

        return await errorOr.MatchAsync(
            forecast =>
            {
                var updated = forecast.Update(request.Date, request.TemperatureC, request.Summary);
                
                return _repository.Update(updated, cancellationToken);
            },
            (errors) =>
            {
                var error = ErrorOr<Updated<Guid>>.From(Error.NotFound($"Weather forecast with id {request.Id} was not found"));
                
                return Task.FromResult(error);
            });
    }
}