using SemSnel.Portofolio.Server.Application.Common.Authorisations.Behaviours;
using SemSnel.Portofolio.Server.Application.Common.Caching;
using SemSnel.Portofolio.Server.Application.Common.Logging;
using SemSnel.Portofolio.Server.Application.Common.Performances;
using SemSnel.Portofolio.Server.Application.Common.Persistence;
using SemSnel.Portofolio.Server.Application.Common.UnHandledExceptions;
using SemSnel.Portofolio.Server.Application.Common.Validations;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Features.Queries.Get;

namespace SemSnel.Portofolio.Infrastructure.Common.Mediatr;

/// <summary>
/// Configures the mediatr services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configures the mediatr services.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the mediatr services added. </returns>
    public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<GetWeatherforecastsQuery>();

            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));
        });

        return services;
    }
}