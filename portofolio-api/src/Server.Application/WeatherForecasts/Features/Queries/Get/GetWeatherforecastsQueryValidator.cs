using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Get;

public sealed class GetWeatherforecastsQueryValidator
    : AbstractValidator<GetWeatherforecastsQuery>
{
    public GetWeatherforecastsQueryValidator(IStringLocalizer<GetWeatherforecastsQueryValidator> localizer)
    {
        RuleFor(query => query.Skip)
            .GreaterThanOrEqualTo(1)
            .WithMessage(localizer["Skip must be greater than or equal to 1"]);

        RuleFor(query => query.Take)
            .GreaterThanOrEqualTo(1).WithMessage(localizer["Take must be greater than or equal to 1"])
            .LessThanOrEqualTo(100).WithMessage(localizer["Take must be less than or equal to 100"]);
    }
}