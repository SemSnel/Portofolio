using SemSnel.Portofolio.Domain._Common.Entities.Events.Outbox;
using SemSnel.Portofolio.Domain._Common.TypeAliases;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.Mappings;

/// <summary>
/// A mapping between a <see cref="DomainMessageType"/> and a <see cref="Type"/>
/// This is used to map a <see cref="DomainMessageType"/> to a <see cref="Type"/> and vice versa
/// The type is used to deserialize the <see cref="DomainMessage.Data"/> to the correct type.
/// </summary>
/// <param name="identifierToType"> A mapping from <see cref="DomainMessageType"/> to <see cref="Type"/>.</param>
/// <param name="typeToIdentifier"> A mapping from <see cref="Type"/> to <see cref="DomainMessageType"/>.</param>
/// <returns> The <see cref="OutboxMessageTypeNameMapping"/>. </returns>
public sealed class OutboxMessageTypeNameMapping(
        IReadOnlyDictionary<OutboxMessageType, Type> identifierToType,
        IReadOnlyDictionary<Type, OutboxMessageType> typeToIdentifier)
    : BaseTypeIdentifierMap<OutboxMessageType, IOutboxEvent>(identifierToType, typeToIdentifier)
{
}