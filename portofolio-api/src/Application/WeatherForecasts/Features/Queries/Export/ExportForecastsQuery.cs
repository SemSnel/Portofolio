using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Application.Common.Files;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Export;

public class ExportForecastsQuery : IRequest<ErrorOr<FileDto>>
{
    public int Skip { get; init; } = 1;
    public int Take { get; init; } = 10;
}

public sealed class ExportForecastsHandler : IRequestHandler<ExportForecastsQuery, ErrorOr<FileDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<WeatherForecast, Guid> _readRepository;
    private readonly ICsvService _csvService;
    private readonly IStringLocalizer<ExportForecastsHandler> _localizer;

    public ExportForecastsHandler(IReadRepository<WeatherForecast, Guid> readRepository, ICsvService csvService, IMapper mapper, IStringLocalizer<ExportForecastsHandler> localizer)
    {
        _readRepository = readRepository;
        _csvService = csvService;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ErrorOr<FileDto>> Handle(ExportForecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await _readRepository
            .Get()
            .ProjectTo<WeatherForecastDto>(_mapper)
            .ToPaginatedListAsync(request.Skip, request.Take);
        
        var excel = await _csvService.Export(forecasts.Items, new Dictionary<string, Func<WeatherForecastDto, object>>()
        {
            { _localizer["Due Date"], forecast => forecast.Date },
            { _localizer["Forecasts"], forecast => forecast.TemperatureC },
            { _localizer["TemperatureF"], forecast => forecast.TemperatureF },
            { _localizer["Summary"], forecast => forecast.Summary }
        });

        return excel;
    }
}