using SemSnel.Portofolio.Domain._Common.Enumerations;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

/// <summary>
/// A summary of the weather forecast.
/// </summary>
public sealed class WeatherForecastSummary : BaseEnumeration<WeatherForecastSummary>
{
    private WeatherForecastSummary(int id, string name)
        : base(id, name)
    {
    }

    /// <summary>
    /// A Sunny day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Sunny = new (1, nameof(Sunny));

    /// <summary>
    /// A Cloudy day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Cloudy = new (2, nameof(Cloudy));

    /// <summary>
    /// A Rainy day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Rainy = new (3, nameof(Rainy));

    /// <summary>
    /// A Stormy day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Stormy = new (4, nameof(Stormy));

    /// <summary>
    /// A Snowy day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Snowy = new (5, nameof(Snowy));

    /// <summary>
    /// A Foggy day summary.
    /// </summary>
    public static readonly WeatherForecastSummary Foggy = new (6, nameof(Foggy));

    public static implicit operator string(WeatherForecastSummary summary) => summary.Name;

    public static implicit operator WeatherForecastSummary(string summary)
    {
        var errorOrSummary = Create(summary);

        if (errorOrSummary.IsError)
        {
            throw new ArgumentException(errorOrSummary.FirstError.Description);
        }

        return errorOrSummary.Value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="WeatherForecastSummary"/> class.
    /// </summary>
    /// <param name="summary"> The summary. </param>
    /// <returns> The <see cref="WeatherForecastSummary"/>. </returns>
    public static ErrorOr<WeatherForecastSummary> Create(string summary)
    {
        var all = GetAll();

        var found = all
            .SingleOrDefault(x => x.Name == summary);

        if (found is null)
        {
            return Error.Validation($"Summary {summary} is not valid.");
        }

        return found;
    }
}