using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;

public sealed class UpdateWeatherForecastCommandValidator : AbstractValidator<UpdateWeatherForecastsCommand>
{
    private readonly IStringLocalizer<UpdateWeatherForecastCommandValidator> _localizer;

    public UpdateWeatherForecastCommandValidator(IStringLocalizer<UpdateWeatherForecastCommandValidator> localizer)
    {
        _localizer = localizer;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(_localizer["Id is required"]);

        RuleFor(command => command.Date)
            .NotEmpty()
            .WithMessage(_localizer["Date is required"]);

        RuleFor(command => command.TemperatureC)
            .NotEmpty()
            .WithMessage(_localizer["TemperatureC is required"]);

        RuleFor(command => command.Summary)
            .NotEmpty()
            .WithMessage(_localizer["Summary is required"]);
    }
}