using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Application.WeatherForecasts;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Create;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Export;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;
using SemSnel.Portofolio.Server.Common.Monads;

namespace SemSnel.Portofolio.Server.WeatherForecasts.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [MapToApiVersion("2.0")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherforecastsQuery query)
    {

        throw new NotImplementedException();
        
        var response = await _mediator.Send(query);
        
        return  response.ToOkActionResult();
    }

    [MapToApiVersion("2.0")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastCommand command)
    {
        var response = await _mediator.Send(command);
        
        return  response.ToCreatedActionResult(nameof(Get), new { id = response.Value });
    }
    
    [MapToApiVersion("2.0")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWeatherForecastsCommand command)
    {
        var response = await _mediator.Send(command);

        return response.ToNoContentActionResult();
    }

    [MapToApiVersion("1.0")]
    [Produces("text/csv")]
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportForecastsQuery query)
    {
        var response = await _mediator.Send(query);
        
        return  response.ToFileContentResult();
    }
}