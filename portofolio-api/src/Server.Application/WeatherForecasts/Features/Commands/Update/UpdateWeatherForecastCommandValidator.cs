using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Update;

public sealed class UpdateWeatherForecastCommandValidator : AbstractValidator<UpdateWeatherForecastsCommand>
{
    public UpdateWeatherForecastCommandValidator(IStringLocalizer<UpdateWeatherForecastCommandValidator> localizer)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(localizer["required"]);

        RuleFor(command => command.Date)
            .NotEmpty()
            .WithMessage(localizer["required"]);

        RuleFor(command => command.TemperatureC)
            .NotEmpty()
            .WithMessage(localizer["required"]);

        RuleFor(command => command.Summary)
            .NotEmpty()
            .WithMessage(localizer["required"]);
    }
}