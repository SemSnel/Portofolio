namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

public static class ConfigureServices
{
    public static IServiceCollection AddIdempotency(this IServiceCollection services, IConfiguration configuration)
    {
        // add settings
        services
            .AddOptions<IdempotencySettings>()
            .BindConfiguration(IdempotencySettings.SectionName)
            .ValidateDataAnnotations();
        
        return services
            .AddTransient<IIdempotencyService, IdempotencyService>();
    }
}