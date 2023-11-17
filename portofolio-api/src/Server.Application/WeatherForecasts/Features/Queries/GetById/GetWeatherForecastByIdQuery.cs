using FluentValidation;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.GetById;

public class GetWeatherForecastByIdQuery : IRequest<ErrorOr<WeatherForecastDto>>
{
    public Guid Id { get; init; }
}

public class GetWeatherForecastByIdQueryValidator : AbstractValidator<GetWeatherForecastByIdQuery>
{
    public GetWeatherForecastByIdQueryValidator(IStringLocalizer<GetWeatherForecastByIdQueryValidator> localizer)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(localizer["not empty"]);
    }
}

public sealed class
    GetWeatherForecastByIdHandler(IWeatherForecastsRepository repository, IMapper mapper) : IRequestHandler<GetWeatherForecastByIdQuery, ErrorOr<WeatherForecastDto>>
{
    public async Task<ErrorOr<WeatherForecastDto>> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        var errorOr = await repository
            .GetById(request.Id, cancellationToken);

        return errorOr.Match(
            forecast => mapper.Map<WeatherForecastDto>(forecast),
            ErrorOr<WeatherForecastDto>.From);
    }
}