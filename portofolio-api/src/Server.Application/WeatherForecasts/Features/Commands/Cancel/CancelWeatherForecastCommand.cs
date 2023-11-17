using FluentValidation;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Server.Application.Common.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Cancel;

public readonly record struct CancelWeatherForecastCommand(Guid Id) : IRequest<ErrorOr<Success>>;

public class CancelWeatherForecastCommandValidator : AbstractValidator<CancelWeatherForecastCommand>
{
    public CancelWeatherForecastCommandValidator(IStringLocalizer<CancelWeatherForecastCommandValidator> localizer)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(localizer["not empty"]);
    }
}

public class CancelWeatherForecastHandler(
        IWeatherForecastsRepository repository)
    : IRequestHandler<CancelWeatherForecastCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CancelWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var errorOrFound = await repository.GetById(request.Id, cancellationToken);

        return await errorOrFound.MatchAsync(HandleFound, HandleNotFound);
    }

    private async Task<ErrorOr<Success>> HandleFound(WeatherForecast weatherForecast)
    {
        weatherForecast.Cancel();

        var errorOrUpdated = await repository.Update(weatherForecast);

        return errorOrUpdated;
    }

    private Task<ErrorOr<Success>> HandleNotFound(IEnumerable<Error> errors)
    {
        return Task.FromResult<ErrorOr<Success>>(Error.Failure("Weather forecast not found"));
    }
}