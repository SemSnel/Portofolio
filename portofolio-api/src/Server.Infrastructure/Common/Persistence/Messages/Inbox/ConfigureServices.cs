using System.Reflection;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Inbox;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities.ValueObjects;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Mappings;
using SemSnel.Portofolio.Server.Application.Common.Reflection;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox;

/// <summary>
/// Contains extension methods for configuring the inbox messages mappings.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configures the inbox messages mappings.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the mappings to.</param>
    /// <param name="assemblies"> The assemblies to scan for <see cref="IInboxEvent"/> implementations.</param>
    /// <returns> The <see cref="IServiceCollection"/>. </returns>
    public static IServiceCollection AddInboxMessagesMappings(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var eventType = typeof(IInboxEvent);

        var eventTypes = assemblies
            .SelectMany(assembly =>
                assembly
                    .GetTypes()
                    .Where(type => type is { IsClass: true, IsAbstract: false }
                                   && type.InheritsOrImplements(eventType)))
            .ToList();

        var typeNameDictionary = eventTypes
            .ToDictionary(type => type, type => new InboxMessageType(type.Name));

        var typeFromNameDictionary = eventTypes
            .ToDictionary(type => new InboxMessageType(type.Name), type => type);

        services.AddSingleton<InboxMessageTypeNameMapping>(
            provider => new InboxMessageTypeNameMapping(typeFromNameDictionary, typeNameDictionary));

        return services;
    }
}