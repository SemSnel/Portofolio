using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

public record GetWeatherforecastsQuery(int Skip = 1, int Take = 10)
    : IRequest<ErrorOr<IEnumerable<WeatherForecastDto>>>;

public sealed class GetWeatherforecastsQueryValidator : AbstractValidator<GetWeatherforecastsQuery>
{
    private readonly IStringLocalizer<GetWeatherforecastsQueryValidator> _localizer;

    public GetWeatherforecastsQueryValidator(IStringLocalizer<GetWeatherforecastsQueryValidator> localizer)
    {
        _localizer = localizer;
        
        RuleFor(query => query.Skip)
            .GreaterThanOrEqualTo(1)
            .WithMessage(_localizer["Skip must be greater than or equal to 1"]);
        
        RuleFor(query => query.Take)
            .GreaterThanOrEqualTo(1)
            .WithMessage(_localizer["Take must be greater than or equal to 1"]);
    }
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

        var forecasts = await _readRepository
            .Get()
            .ProjectTo<WeatherForecastDto>(_mapper)
            .ToPaginatedListAsync(request.Skip, request.Take);

        return ErrorOr.From(forecasts.Items);
    }
}