using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

namespace SemSnel.Portofolio.Infrastructure.Common.Mediatr;

public static class ConfigureServices
{
    public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<GetWeatherforecastsQuery>();
            configuration.Lifetime = ServiceLifetime.Transient;
        });
        
        return services;
    }
}