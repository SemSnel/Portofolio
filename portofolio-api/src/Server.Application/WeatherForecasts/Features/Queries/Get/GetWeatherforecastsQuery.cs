using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Pagination;
using SemSnel.Portofolio.Server.Application.Common.Caching;
using SemSnel.Portofolio.Server.Application.Common.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Get;

public sealed class GetWeatherforecastsQuery : IRequest<ErrorOr<PaginatedList<WeatherForecastDto>>>, ICacheableRequest
{
    public int Skip { get; init; } = 1;

    public int Take { get; init; } = 10;

    public string GetCacheKey() => $"GetWeatherforecastsQuery:{Skip}:{Take}";

    public TimeSpan? GetTimeToLive() => TimeSpan.FromDays(1);
}

public sealed class
    GetWeatherforecastsHandler(IMapper mapper, IWeatherForecastsRepository readRepository) : IRequestHandler<GetWeatherforecastsQuery, ErrorOr<PaginatedList<WeatherForecastDto>>>
{
    public async Task<ErrorOr<PaginatedList<WeatherForecastDto>>> Handle(GetWeatherforecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await
            readRepository
                .Get()
                .ProjectTo<WeatherForecastDto>(mapper)
                .ToPaginatedListAsync(request.Skip, request.Take);

        return ErrorOr.From(forecasts);
    }
}