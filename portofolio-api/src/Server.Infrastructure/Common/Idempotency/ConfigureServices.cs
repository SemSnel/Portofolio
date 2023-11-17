namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

/// <summary>
/// Configures the idempotency services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the idempotency services to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the idempotency services added. </returns>
    public static IServiceCollection AddIdempotency(this IServiceCollection services, IConfiguration configuration)
    {
        // add settings
        services
            .AddOptions<IdempotencySettings>()
            .BindConfiguration(IdempotencySettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services
            .AddTransient<IIdempotencyService, IdempotencyService>();
    }
}