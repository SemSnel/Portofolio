using Microsoft.AspNetCore.Mvc;
using SemSnel.Portofolio.Application.WeatherForecasts;

namespace SemSnel.Portofolio.Server.WeatherForecasts.v2;

[ApiController]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
    [MapToApiVersion("2.0")]
    [HttpGet]
    public IEnumerable<WeatherForecastDto> Get()
    {
        return Enumerable.Range(1, 1).Select(index => new WeatherForecastDto
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(10, 11),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}