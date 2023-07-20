using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Application.WeatherForecasts;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Create;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Export;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.WeatherForecasts.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [MapToApiVersion("1.0")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherforecastsQuery query)
    {
        var errorOr = await _mediator.Send(query);
        
        return  errorOr.Match<IActionResult>(
            forecasts => Ok(forecasts),
            errors => errors.First().Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Conflict => Conflict(),
                ErrorType.Unauthorized => Unauthorized(),
                ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
                ErrorType.Validation => BadRequest(errors.First().Description),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
            });
    }
    
    [MapToApiVersion("1.0")]
    [HttpGet("sql")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherforecastsBySqlQuery query)
    {
        var errorOr = await _mediator.Send(query);
        
        return  errorOr.Match<IActionResult>(
            forecasts => Ok(forecasts),
            errors => errors.First().Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Conflict => Conflict(),
                ErrorType.Unauthorized => Unauthorized(),
                ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
                ErrorType.Validation => BadRequest(errors.First().Description),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            });
    }
    
    [MapToApiVersion("1.0")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastCommand command)
    {
        var errorOr = await _mediator.Send(command);
        
        return  errorOr.Match<IActionResult>(
            forecast => CreatedAtAction(nameof(Get), new { id = forecast.Value }, forecast),
            errors => errors.First().Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Conflict => Conflict(),
                ErrorType.Unauthorized => Unauthorized(),
                ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
                ErrorType.Validation => BadRequest(errors.First().Description),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
            });
    }


    [MapToApiVersion("1.0")]
    [Produces("text/csv")]
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportForecastsQuery query)
    {
        var errorOr = await _mediator.Send(query);
        
        return  errorOr.Match<IActionResult>(
            fileDto => File(fileDto.Content, "text/csv", "weather-forecasts.csv"),
            errors => errors.First().Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Conflict => Conflict(),
                ErrorType.Unauthorized => Unauthorized(),
                ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
                ErrorType.Validation => BadRequest(errors.First().Description),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
            });
    }
}