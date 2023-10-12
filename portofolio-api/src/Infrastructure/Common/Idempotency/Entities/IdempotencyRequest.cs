namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

public record IdempotentRequest
{
    public Guid Id { get; init; }
    
    public string Name { get; init; } = string.Empty;
    
    public System.DateTime CreatedOn { get; init; }
}