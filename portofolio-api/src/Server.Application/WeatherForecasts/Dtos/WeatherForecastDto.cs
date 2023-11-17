namespace SemSnel.Portofolio.Server.Application.WeatherForecasts.Dtos;

public record WeatherForecastDto(
    DateTime Date,
    int TemperatureC,
    int TemperatureF,
    string Summary);