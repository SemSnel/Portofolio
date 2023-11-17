using Mapster;

namespace SemSnel.Portofolio.Infrastructure.Common.Mapping;

public static class ConfigureServices
{
    public static IServiceCollection AddMapping(this IServiceCollection services, IConfiguration configuration)
    {
        var mapperConfig = new TypeAdapterConfig();

        TypeAdapterConfig.GlobalSettings.EnableImmutableMapping();

        mapperConfig.EnableImmutableMapping();

        services.AddScoped<IMapper>(sp => new ServiceMapper(sp, mapperConfig));

        return services;
    }
}