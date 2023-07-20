using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public class UpdateWeatherForecastsCommand : IRequest<ErrorOr<Updated<Guid>>>
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}

public class UpdateWeatherForecastsCommandHandler : IRequestHandler<UpdateWeatherForecastsCommand, ErrorOr<Updated<Guid>>>
{
    private readonly IReadRepository<WeatherForecast, Guid> _readRepository;
    private readonly IWriteRepository<WeatherForecast, Guid> _repository;

    public UpdateWeatherForecastsCommandHandler(IWriteRepository<WeatherForecast, Guid> repository, IReadRepository<WeatherForecast, Guid> readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }

    public async Task<ErrorOr<Updated<Guid>>> Handle(UpdateWeatherForecastsCommand request, CancellationToken cancellationToken)
    {
        var errorOr = await _readRepository.GetById(request.Id, cancellationToken);
        
        if (errorOr.IsError)
        {
            return Error.NotFound($"Weather forecast with id {request.Id} was not found");
        }
        
        var weatherForecast = errorOr.Value;
        
        var update = weatherForecast.Update(request.Date, request.TemperatureC, request.Summary);
        
        var result = await _repository.Update(update, cancellationToken);

        return result;
    }
}