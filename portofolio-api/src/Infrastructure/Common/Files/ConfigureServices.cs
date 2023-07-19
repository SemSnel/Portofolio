using SemSnel.Portofolio.Application.Common.Files;

namespace SemSnel.Portofolio.Infrastructure.Common.Files;

public static class ConfigureServices
{
    public static IServiceCollection AddFileServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ICsvService, CsvService>();
    }
}