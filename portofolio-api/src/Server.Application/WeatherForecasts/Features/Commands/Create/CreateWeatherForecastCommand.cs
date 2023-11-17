using FluentValidation;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.NoneOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;
using SemSnel.Portofolio.Server.Application.Common.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Create;

public record CreateWeatherForecastCommand(
    DateTime Date,
    int TemperatureC,
    string Summary
) : IRequest<ErrorOr<Created<Guid>>>;

public sealed class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
{
    public CreateWeatherForecastCommandValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(x => x.TemperatureC)
            .NotEmpty()
            .WithMessage("Temperature is required.");

        var summaries = WeatherForecastSummary.GetAll().Select(x => x.Name);
        var options = string.Join(", ", summaries);

        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithMessage("Summary is required.")
            .Must(x => WeatherForecastSummary.GetAll().Any(y => y.Name == x))
            .WithMessage($"Summary is not valid. Valid options are: {options}");
    }
}

public sealed class
    CreateWeatherForecastCommandHandler(
        IWeatherForecastsRepository writeRepository)
    : IRequestHandler<CreateWeatherForecastCommand, ErrorOr<Created<Guid>>>
{
    public async Task<ErrorOr<Created<Guid>>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var summary = WeatherForecastSummary.Create(request.Summary);

        if (summary.IsError)
        {
            return summary.FirstError;
        }

        var forecasts = WeatherForecast
            .Create(request.Date, request.TemperatureC, summary.Value);

        if (forecasts.IsError)
        {
            return forecasts.FirstError;
        }

        var errorOrCreated = await writeRepository
            .Add(forecasts.Value, cancellationToken);

        return errorOrCreated;
    }
}