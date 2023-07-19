using SemSnel.Portofolio.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.DateTime;

public static class ConfigureServices
{
    public static IServiceCollection AddDateTimeServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }
}