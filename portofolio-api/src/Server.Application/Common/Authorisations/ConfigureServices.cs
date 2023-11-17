using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Server.Application.Common.Authorisations.Authorizers;
using SemSnel.Portofolio.Server.Application.Common.Reflection;

namespace SemSnel.Portofolio.Server.Application.Common.Authorisations;

/// <summary>
/// Configures the services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the authorisations to the services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <returns> The services with the authorisations added. </returns>
    public static IServiceCollection AddAuthorisations(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;

        var authorisors =
            assembly
                .GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false }
                            && t.InheritsOrImplements(typeof(IAuthorizer<>)))
                .ToList();

        foreach (var authorisor in authorisors)
        {
            var genericType = authorisor
                .GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAuthorizer<>))
                .GetGenericArguments()
                .First();

            var genericInterface = typeof(IAuthorizer<>).MakeGenericType(genericType);

            services.AddScoped(genericInterface, authorisor);
        }

        return services;
    }
}