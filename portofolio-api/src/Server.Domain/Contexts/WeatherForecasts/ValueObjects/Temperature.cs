namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

public record Temperature(int Value)
{
    public static implicit operator int(Temperature temperature) => temperature.Value;
    public static implicit operator Temperature(int temperature) => new (temperature);

    /// <summary>
    /// Creates a new instance of the <see cref="Temperature"/> class.
    /// </summary>
    /// <param name="temp"> The temperature. </param>
    /// <returns> The <see cref="Temperature"/>. </returns>
    public static Temperature Create(int temp) => new Temperature(temp);
}