using FluentValidation;
using SemSnel.Portofolio.Server.Application.Common.Validations;

namespace SemSnel.Portofolio.Infrastructure.Common.Validations;

/// <summary>
/// A class that configures the services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configures the validation services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <param name="configuration"> The configuration. </param>
    /// <returns> The services with the validation services added. </returns>
    public static IServiceCollection AddValidationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[]
        {
            typeof(ConfigureServices).Assembly,
            typeof(ValidationBehaviour<,>).Assembly,
        };

        return services
            .AddValidatorsFromAssemblies(assemblies);
    }
}