using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Entities.Auditability;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Events;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;

/// <summary>
/// A weather forecast.
/// </summary>
public class WeatherForecast : BaseAggregateRoot<Guid>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature.
    /// </summary>
    public Temperature TemperatureC { get; set; } = null!;

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public WeatherForecastSummary? Summary { get; set; }

    /// <inheritdoc/>
    public Guid? CreatedBy { get; set; }

    /// <inheritdoc/>
    public DateTime CreatedOn { get; set; }

    /// <inheritdoc/>
    public Guid? LastModifiedBy { get; set; }

    /// <inheritdoc/>
    public DateTime? LastModifiedOn { get; set; }

    /// <inheritdoc/>
    public Guid? DeletedBy { get; set; }

    /// <inheritdoc/>
    public DateTime? DeletedOn { get; set; }

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="requestDate"> The date. </param>
    /// <param name="temperature"> The temperature. </param>
    /// <param name="requestSummary"> The summary. </param>
    /// <returns> The <see cref="ErrorOr{WeatherForecast}"/>. </returns>
    public static ErrorOr<WeatherForecast> Create(DateTime requestDate, Temperature temperature, WeatherForecastSummary? requestSummary)
    {
        var forecasts = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = requestDate,
            TemperatureC = temperature,
            Summary = requestSummary,
        };

        var message = new WeatherForecastCreatedDomainEvent(
            forecasts.Id,
            forecasts.Summary is null ? null : forecasts.Summary.Name,
            forecasts.TemperatureC);

        forecasts.AddDomainEvent(message);

        return forecasts;
    }

    /// <summary>
    /// Updates the weather forecast.
    /// </summary>
    /// <param name="requestDate"> The date. </param>
    /// <param name="requestTemperatureC"> The temperature. </param>
    /// <param name="requestSummary"> The summary. </param>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    public ErrorOr<Success> Update(DateTime requestDate, int requestTemperatureC, WeatherForecastSummary? requestSummary)
    {
        Date = requestDate;
        TemperatureC = requestTemperatureC;
        Summary = requestSummary;

        var message = new WeatherForecastUpdatedDomainEvent(this.Id);

        AddDomainEvent(message);

        return Result.Success;
    }

    /// <summary>
    /// Cancels the weather forecast.
    /// </summary>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    public ErrorOr<Success> Cancel()
    {
        if (DeletedOn.HasValue)
        {
            return Errors.WeatherForecast.AlreadyCancelled;
        }

        DeletedOn = DateTime.UtcNow;
        DeletedBy = Id;

        var message = new WeatherForecastDeletedDomainEvent(Id);

        AddDomainEvent(message);

        return Result.Success;
    }
}