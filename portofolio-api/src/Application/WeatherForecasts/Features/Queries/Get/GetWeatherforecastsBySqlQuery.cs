using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

public record GetWeatherforecastsBySqlQuery(int Skip = 1, int Take = 10)
    : IRequest<ErrorOr<IEnumerable<WeatherForecastDto>>>;

public sealed class
    GetWeatherforecastsBySqlQueryHandler : IRequestHandler<GetWeatherforecastsBySqlQuery, ErrorOr<IEnumerable<WeatherForecastDto>>>
{
    private readonly IMapper _mapper;
    private readonly ISearchableReadRepository<WeatherForecast, Guid> _readRepository;

    public GetWeatherforecastsBySqlQueryHandler(IMapper mapper, ISearchableReadRepository<WeatherForecast, Guid> readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ErrorOr<IEnumerable<WeatherForecastDto>>> Handle(GetWeatherforecastsBySqlQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM WeatherForecasts";
        // add skip and take
        //sql += $" OFFSET {request.Skip} ROWS FETCH NEXT {request.Take} ROWS ONLY";

        var forecasts = await _readRepository.Search<WeatherForecastDto>(sql);

        return forecasts;
    }
}