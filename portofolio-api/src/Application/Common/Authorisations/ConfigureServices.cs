using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Application.Common.Authorisations.Authorizers;
using SemSnel.Portofolio.Application.Common.Reflection;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Application.Common.Authorisations;

public static class ConfigureServices
{
    public static IServiceCollection AddAuthorisations(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;
        
        // get all the authorisors also check if the parent classes has the generic type IAuthorizor<>
        
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

