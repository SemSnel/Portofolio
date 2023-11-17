using System.Reflection;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Mappings;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;
using SemSnel.Portofolio.Server.Application.Common.Reflection;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain;

/// <summary>
/// Configure services for the domain messages.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configure services for the domain messages.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="assemblies"> The assemblies to scan for domain events. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the domain messages configured. </returns>
    public static IServiceCollection AddDomainMessagesMappings(this IServiceCollection services, params Assembly[] assemblies)
    {
        var eventType = typeof(IDomainEvent);

        var eventTypes = assemblies
            .SelectMany(assembly =>
                assembly
                    .GetTypes()
                    .Where(type => type is not(TypeInfo { IsAbstract: true } or { IsInterface: true })
                                   && type is TypeInfo { IsClass: true } or { IsValueType: true }
                                   && type.InheritsOrImplements(eventType)))
            .ToList();

        var typeNameDictionary = eventTypes
            .ToDictionary(type => type, type => new DomainMessageType(type.Name));

        var typeFromNameDictionary = eventTypes
            .ToDictionary(type => new DomainMessageType(type.Name), type => type);

        services.AddSingleton<DomainMessageTypeNameMapping>(
            provider => new DomainMessageTypeNameMapping(typeFromNameDictionary, typeNameDictionary));

        return services;
    }
}