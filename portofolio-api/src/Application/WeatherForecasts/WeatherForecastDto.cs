namespace SemSnel.Portofolio.Application.WeatherForecasts;

public class WeatherForecastDto
{
    public DateOnly Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF { get; init;}

    public string? Summary { get; init; }
}