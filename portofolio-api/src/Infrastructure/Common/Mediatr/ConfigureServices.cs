using SemSnel.Portofolio.Application.Common.Authorisations;
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
        services.AddMediatR(options =>
        {
            // register handlers
            options.RegisterServicesFromAssemblyContaining<GetWeatherforecastsQuery>();
            
            // add behaviours
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(PerformanceBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(LoggingBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(AuthorizationBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(UnHandledExceptionBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
        });
        
        return services;
    }
}