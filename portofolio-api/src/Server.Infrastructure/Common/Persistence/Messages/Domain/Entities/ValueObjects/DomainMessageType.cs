namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;

public readonly record struct DomainMessageType(string Value)
{
    public static implicit operator string(DomainMessageType domainMessageType) => domainMessageType.Value;
    public static implicit operator DomainMessageType(string value) => new (value);
}