using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Dtos;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Create;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Commands.Update;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Export;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Get;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.GetById;
using SemSnel.Portofolio.Server.Common.Idempotency;
using SemSnel.Portofolio.Server.Common.Monads;
using Swashbuckle.AspNetCore.Annotations;

namespace SemSnel.Portofolio.Server.WeatherForecasts.v1;

/// <summary>
/// Controller for the weather forecasts.
/// </summary>
/// <param name="mediator"> The <see cref="ISender"/>. </param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController(ISender mediator) : ControllerBase
{
    [MapToApiVersion("1.0")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherforecastsQuery query)
    {
        var response = await mediator.Send(query);

        return response.ToOkActionResult();
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
        var response = await mediator.Send(new GetWeatherForecastByIdQuery { Id = id });

        return response.ToOkActionResult();
    }

    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [IdempotencyFilter]
    [SwaggerOperation(
        Summary = "Creates a new weather forecast",
        Description = "Creates a new weather forecast",
        OperationId = "WeatherForecast.Create",
        Tags = new[] { "WeatherForecastEndpoint" })
    ]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastCommand command)
    {
        var response = await mediator.Send(command);

        return response.ToCreatedActionResult(nameof(Get), new { id = response.Value });
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

        var response = await mediator.Send(commandWithId);

        return response.ToNoContentActionResult();
    }

    [MapToApiVersion("1.0")]
    [Produces("text/csv")]
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportForecastsQuery query)
    {
        var response = await mediator
            .Send(query);

        return response.ToFileContentResult();
    }
}
