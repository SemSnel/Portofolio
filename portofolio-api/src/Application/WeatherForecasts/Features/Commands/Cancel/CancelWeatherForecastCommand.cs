using FluentValidation;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Cancel;

public class CancelWeatherForecastCommand : IRequest<ErrorOr<Success>>
{
    public Guid Id { get; init; }
}

public class CancelWeatherForecastCommandValidator : AbstractValidator<CancelWeatherForecastCommand>
{
    public CancelWeatherForecastCommandValidator(IStringLocalizer<CancelWeatherForecastCommandValidator> localizer)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(localizer["not empty"]);
    }
}

public class CancelWeatherForecastHandler : IRequestHandler<CancelWeatherForecastCommand, ErrorOr<Success>>
{
    private readonly IWeatherForecastsRepository _repository;

    public CancelWeatherForecastHandler(IWeatherForecastsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Success>> Handle(CancelWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var errorOr = await _repository.GetById(request.Id, cancellationToken);

        if (errorOr.IsError)
        {
            return errorOr.FirstError;
        }
        
        var forecast = errorOr.Value;
        
        forecast.Cancel();
        
        var updated = _repository.Delete(forecast, cancellationToken);

        if (updated.IsError)
        {
            return updated.FirstError;
        }

        return Result.Success();
    }
}

