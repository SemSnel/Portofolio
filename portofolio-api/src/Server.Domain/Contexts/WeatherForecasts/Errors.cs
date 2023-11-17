using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;

/// <summary>
/// Error definitions for the WeatherForecast context.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// The WeatherForecast errors.
    /// </summary>
    public static class WeatherForecast
    {
        /// <summary>
        /// Represents a WeatherForecast not found error.
        /// </summary>
        public static readonly Error NotFound = Error.NotFound("WeatherForecast.NotFound", "WeatherForecast not found.");

        /// <summary>
        /// Represents a WeatherForecast already cancelled error.
        /// </summary>
        public static readonly Error AlreadyCancelled = Error.Validation("WeatherForecast.AlreadyCancelled", "WeatherForecast is already cancelled.");

        /// <summary>
        /// Represents a WeatherForecast already deleted error.
        /// </summary>
        public static readonly Error AlreadyDeleted = Error.Validation("WeatherForecast.AlreadyDeleted", "WeatherForecast is already deleted.");
    }
}