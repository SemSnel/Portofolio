using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

public sealed class GetWeatherforecastsQueryValidator 
    : AbstractValidator<GetWeatherforecastsQuery>
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