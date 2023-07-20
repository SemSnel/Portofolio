using MapsterMapper;

namespace SemSnel.Portofolio.Infrastructure.Common.Mapping;

public static class ConfigureServices
{
    public static IServiceCollection AddMapping(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMapper, Mapper>();
        
        return services;
    }
}