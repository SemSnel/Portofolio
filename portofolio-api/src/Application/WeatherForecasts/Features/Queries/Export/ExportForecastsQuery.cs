using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Application.Common.Files;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Export;

public class ExportForecastsQuery : IRequest<ErrorOr<FileDto>>
{
}

public sealed class ExportForecastsHandler : IRequestHandler<ExportForecastsQuery, ErrorOr<FileDto>>
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastsRepository _readRepository;
    private readonly ICsvService _csvService;
    private readonly IStringLocalizer<ExportForecastsHandler> _localizer;

    public ExportForecastsHandler(ICsvService csvService, IMapper mapper, IStringLocalizer<ExportForecastsHandler> localizer, IWeatherForecastsRepository readRepository)
    {
        _csvService = csvService;
        _mapper = mapper;
        _localizer = localizer;
        _readRepository = readRepository;
    }

    public async Task<ErrorOr<FileDto>> Handle(ExportForecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await _readRepository
            .Get()
            .ProjectTo<WeatherForecastDto>(_mapper)
            .ToListAsync(cancellationToken);
        
        var fileName = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-weatherforecasts.csv";
        
        var excel = await _csvService.Export(forecasts, new Dictionary<string, Func<WeatherForecastDto, object>>()
        {
            { _localizer["Due Date"], forecast => forecast.Date },
            { _localizer["Forecasts"], forecast => forecast.TemperatureC },
            { _localizer["TemperatureF"], forecast => forecast.TemperatureF },
            { _localizer["Summary"], forecast => forecast.Summary }
        }, fileName);

        return excel;
    }
}