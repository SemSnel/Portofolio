using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Create;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Commands.Update;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Export;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.GetById;
using SemSnel.Portofolio.Server.Common.Idempotency;
using SemSnel.Portofolio.Server.Common.Monads;
using Swashbuckle.AspNetCore.Annotations;

namespace SemSnel.Portofolio.Server.WeatherForecasts.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ISender _mediator;

    public WeatherForecastController(ISender mediator)
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
        var response = await _mediator.Send(query);
        
        return  response.ToOkActionResult();
    }
    
    [MapToApiVersion("1.0")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetWeatherForecastByIdQuery { Id = id });
        
        return  response.ToOkActionResult();
    }

    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    //idempotency filter
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // add idempotency filter and idempotency header to swagger
    [IdempotencyFilter]
    [SwaggerOperation(
        Summary = "Creates a new weather forecast",
        Description = "Creates a new weather forecast",
        OperationId = "WeatherForecast.Create",
        Tags = new[] { "WeatherForecastEndpoint" })
    ]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastCommand command)
    {
        var response = await _mediator.Send(command);
        
        return  response.ToCreatedActionResult(nameof(Get), new { id = response.Value });
    }
    
    [MapToApiVersion("1.0")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWeatherForecastsCommand command)
    {
        var commandWithId = command with { Id = id };
        
        var response = await _mediator.Send(commandWithId);

        return response.ToNoContentActionResult();
    }

    [MapToApiVersion("1.0")]
    [Produces("text/csv")]
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportForecastsQuery query)
    {
        var response = await _mediator
            .Send(query);
        
        return  response.ToFileContentResult();
    }
}
