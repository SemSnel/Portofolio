namespace SemSnel.Portofolio.Application.WeatherForecasts;

public class WeatherForecastDto
{
    public Guid Id { get; init; }
    
    public DateOnly Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF { get; init;}

    public string? Summary { get; init; }
    
    public DateTime CreatedOn { get; init; }
    
    public DateTime LastModifiedOn { get; init; }
}