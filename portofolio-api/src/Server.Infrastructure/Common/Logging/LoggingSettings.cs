namespace SemSnel.Portofolio.Infrastructure.Common.Logging;

/// <summary>
/// Settings for logging.
/// </summary>
public class LoggingSettings
{
    /// <summary>
    /// The section name for the logging settings. Is used in the appsettings.json file.
    /// </summary>
    public const string SectionName = "Logging";

    /// <summary>
    /// Gets or sets the default log path.
    /// </summary>
    public string? LogPath { get; set; } = "logs/log.txt";

    /// <summary>
    /// Gets or sets the default log path format.
    /// </summary>
    public string? LogPathFormat { get; set; } = "logs/log-{Date}.txt";

    /// <summary>
    /// Gets or sets the default log path date format.
    /// </summary>
    public string? LogPathDateFormat { get; set; } = "yyyyMMdd";

    /// <summary>
    /// Gets or sets the default log level.
    /// </summary>
    public string? LogLevel { get; set; } = "Information";

    /// <summary>
    /// Gets or sets the Sentry DSN.
    /// </summary>
    public string? SentryDsn { get; set; } = null;
}