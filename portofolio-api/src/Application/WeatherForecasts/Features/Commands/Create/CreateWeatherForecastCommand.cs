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
    private readonly IUnitOfWork _unitOfWork;

    public CreateWeatherForecastCommandHandler(IWeatherForecastsRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created<Guid>>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var forecasts = WeatherForecast.Create(request.Date, request.TemperatureC, request.Summary);
        
        var createdResult = _writeRepository.Add(forecasts, cancellationToken);
        
        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return saveResult.IsError ? saveResult.FirstError : createdResult;
    }
}