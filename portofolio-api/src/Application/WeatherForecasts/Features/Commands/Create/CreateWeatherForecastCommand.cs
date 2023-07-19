using SemSnel.Portofolio.Application.Common.Persistence;
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
    private readonly IWriteRepository<WeatherForecast, Guid> _writeRepository;

    public CreateWeatherForecastCommandHandler(IWriteRepository<WeatherForecast, Guid> writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task<ErrorOr<Created<Guid>>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var forecasts = WeatherForecast.Create(request.Date, request.TemperatureC, request.Summary);
        
        var errorOr = await _writeRepository.Add(forecasts, cancellationToken);

        var result = errorOr.Value;
        
        return result;
    }
}