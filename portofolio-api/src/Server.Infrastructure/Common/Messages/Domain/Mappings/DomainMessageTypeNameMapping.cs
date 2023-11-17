using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Domain._Common.TypeAliases;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Mappings;

/// <summary>
/// A mapping between a <see cref="DomainMessageType"/> and a <see cref="Type"/>
/// This is used to map a <see cref="DomainMessageType"/> to a <see cref="Type"/> and vice versa
/// The type is used to deserialize the <see cref="DomainMessage.Data"/> to the correct type.
/// </summary>
/// <param name="identifierToType"> The <see cref="DomainMessageType"/> to <see cref="Type"/> mapping. </param>
/// <param name="typeToIdentifier"> The <see cref="Type"/> to <see cref="DomainMessageType"/> mapping. </param>
public sealed class DomainMessageTypeNameMapping(
        IReadOnlyDictionary<DomainMessageType, Type> identifierToType,
        IReadOnlyDictionary<Type, DomainMessageType> typeToIdentifier)
    : BaseTypeIdentifierMap<DomainMessageType, IDomainEvent>(identifierToType, typeToIdentifier)
{
}