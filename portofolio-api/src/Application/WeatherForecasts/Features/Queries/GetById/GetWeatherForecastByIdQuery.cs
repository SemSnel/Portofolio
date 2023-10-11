using FluentValidation;
using Microsoft.Extensions.Localization;
using SemSnel.Portofolio.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.GetById;

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
    GetWeatherForecastByIdHandler : IRequestHandler<GetWeatherForecastByIdQuery, ErrorOr<WeatherForecastDto>>
{
    private readonly IWeatherForecastsRepository _repository;
    private readonly IMapper _mapper;

    public GetWeatherForecastByIdHandler(IWeatherForecastsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<WeatherForecastDto>> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        var errorOr = await _repository
            .GetById(request.Id, cancellationToken);

        return errorOr.Match(
            forecast => _mapper.Map<WeatherForecastDto>(forecast),
            ErrorOr<WeatherForecastDto>.From
            );
    }
}