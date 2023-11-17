using System.Text;
using Serilog;
using Serilog.Events;

namespace SemSnel.Portofolio.Infrastructure.Common.Logging;

/// <summary>
/// Configure logging services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Add logging services.
    /// </summary>
    /// <param name="services">services.</param>
    /// <param name="configuration">configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        // add settings
        services
            .AddOptions<LoggingSettings>()
            .BindConfiguration(LoggingSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var loggingSettings = configuration.GetSection(LoggingSettings.SectionName).Get<LoggingSettings>();

        services
            .AddLogging(builder => { builder.AddSerilog(dispose: true); });

        if (loggingSettings == null)
        {
            return services;
        }

        var logConfiguration = new LoggerConfiguration()
            .Enrich
            .FromLogContext()
            .WriteTo
            .Console()
            .WriteTo
            .File(
                path: loggingSettings.LogPath ?? "logs/log.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(loggingSettings.LogLevel ?? "Information"),
                fileSizeLimitBytes: 10000000,
                rollOnFileSizeLimit: true,
                shared: false,
                flushToDiskInterval: TimeSpan.FromSeconds(1),
                retainedFileCountLimit: 31,
                encoding: Encoding.UTF8,
                buffered: true);

        if (!string.IsNullOrEmpty(loggingSettings.SentryDsn))
        {
            logConfiguration
                .WriteTo
                .Sentry(o =>
                {
                    // use settings for Sentry
                    o.MinimumBreadcrumbLevel = Enum.Parse<LogEventLevel>(loggingSettings.LogLevel ?? "Information");
                    o.MinimumEventLevel = Enum.Parse<LogEventLevel>(loggingSettings.LogLevel ?? "Information");
                    o.AttachStacktrace = true;
                    o.Dsn = loggingSettings.SentryDsn;
                });
        }

        Log.Logger = logConfiguration
            .CreateLogger();

        return services;
    }
}