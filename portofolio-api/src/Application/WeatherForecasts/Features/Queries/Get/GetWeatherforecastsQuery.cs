using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

public record GetWeatherforecastsQuery(int Skip = 1, int Take = 10)
    : IRequest<ErrorOr<IEnumerable<WeatherForecastDto>>>;

public sealed class
    GetWeatherforecastsHandler : IRequestHandler<GetWeatherforecastsQuery, ErrorOr<IEnumerable<WeatherForecastDto>>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<WeatherForecast, Guid> _readRepository;

    public GetWeatherforecastsHandler(IMapper mapper, IReadRepository<WeatherForecast, Guid> readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ErrorOr<IEnumerable<WeatherForecastDto>>> Handle(GetWeatherforecastsQuery request, CancellationToken cancellationToken)
    {

        var forecasts = await _readRepository
            .Get()
            .ProjectTo<WeatherForecastDto>(_mapper)
            .ToPaginatedListAsync(request.Skip, request.Take);

        return ErrorOr.From(forecasts.Items);
    }
}