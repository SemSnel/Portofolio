using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Create;

public sealed class CreateWeatherForecastCommand : IRequest<ErrorOr<Created<Guid>>>
{
    public DateOnly Date { get; init; }

    public int TemperatureC { get; init; }

    public string? Summary { get; init; }
}

public sealed class
    CreateWeatherForecastCommandHandler : IRequestHandler<CreateWeatherForecastCommand, ErrorOr<Created<Guid>>>
{
    private readonly IWeatherForecastsRepository _writeRepository;

    public CreateWeatherForecastCommandHandler(IWeatherForecastsRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task<ErrorOr<Created<Guid>>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var forecasts = WeatherForecast.Create(request.Date, request.TemperatureC, request.Summary);
        
        var errorOr = await _writeRepository.Add(forecasts, cancellationToken);

        return errorOr;
    }
}