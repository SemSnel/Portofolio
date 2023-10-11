using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

public sealed class GetWeatherforecastsQuery : IRequest<ErrorOr<IEnumerable<WeatherForecastDto>>>
{
    public int Skip { get; init; } = 1;
    public int Take { get; init; } = 10;
}

public sealed class
    GetWeatherforecastsHandler : IRequestHandler<GetWeatherforecastsQuery, ErrorOr<IEnumerable<WeatherForecastDto>>>
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastsRepository _readRepository;
    
    public GetWeatherforecastsHandler(IMapper mapper, IWeatherForecastsRepository readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ErrorOr<IEnumerable<WeatherForecastDto>>> Handle(GetWeatherforecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await 
            _readRepository.Get()
                .ProjectTo<WeatherForecastDto>(_mapper)
                .ToPaginatedListAsync(request.Skip, request.Take);
        
        return ErrorOr.From(forecasts.Items);
    }
}