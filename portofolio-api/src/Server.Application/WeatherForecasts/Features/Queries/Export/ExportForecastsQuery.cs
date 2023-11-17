using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.Caching;
using SemSnel.Portofolio.Server.Application.Common.Files;
using SemSnel.Portofolio.Server.Application.Common.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Export;

public class ExportForecastsQuery :
    IRequest<ErrorOr<FileDto>>,
    ICacheableRequest
{
    public string GetCacheKey() => nameof(ExportForecastsQuery);

    public TimeSpan? GetTimeToLive() => TimeSpan.FromDays(1);
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

        var mapping = new Dictionary<string, Func<WeatherForecastDto, object>>()
        {
            { _localizer["Due Date"], forecast => forecast.Date },
            { _localizer["Forecasts"], forecast => forecast.TemperatureC },
            { _localizer["TemperatureF"], forecast => forecast.TemperatureF },
            { _localizer["Summary"], forecast => forecast.Summary },
        };

        var excel = await _csvService.Export(forecasts, mapping, fileName);

        return excel;
    }
}