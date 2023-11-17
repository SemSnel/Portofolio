using System.Reflection;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Outbox;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.Mappings;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;
using SemSnel.Portofolio.Server.Application.Common.Reflection;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox;

/// <summary>
/// Configures the outbox messages mappings.
/// </summary>
public static class ConfigureServicesOutbox
{
    /// <summary>
    /// Configures the outbox messages mappings.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="assemblies"> The assemblies to scan for <see cref="IOutboxEvent"/> implementations. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the outbox messages mappings configured. </returns>
    public static IServiceCollection AddOutboxMessagesMappings(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var eventType = typeof(IOutboxEvent);

        var eventTypes = assemblies
            .SelectMany(assembly =>
                assembly
                    .GetTypes()
                    .Where(type => type is { IsClass: true, IsAbstract: false }
                                   && type.InheritsOrImplements(eventType)))
            .ToList();

        var typeNameDictionary = eventTypes
            .ToDictionary(type => type, type => new OutboxMessageType(type.Name));

        var typeFromNameDictionary = eventTypes
            .ToDictionary(type => new OutboxMessageType(type.Name), type => type);

        services.AddSingleton<OutboxMessageTypeNameMapping>(
            provider => new OutboxMessageTypeNameMapping(typeFromNameDictionary, typeNameDictionary));

        return services;
    }
}