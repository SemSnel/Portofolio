using SemSnel.Portofolio.Application.Common.Logging;
using SemSnel.Portofolio.Application.Common.Performances;
using SemSnel.Portofolio.Application.Common.UnHandledExceptions;
using SemSnel.Portofolio.Application.Common.Validations;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Queries.Get;

namespace SemSnel.Portofolio.Infrastructure.Common.Mediatr;

public static class ConfigureServices
{
    public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMediatR(typeof(GetWeatherforecastsQuery).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        
        return services;
    }
}