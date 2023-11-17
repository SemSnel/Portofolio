namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;

public readonly record struct DomainMessageData(string Value)
{
    public static implicit operator string(DomainMessageData domainMessageData) => domainMessageData.Value;
    public static implicit operator DomainMessageData(string value) => new (value);
}