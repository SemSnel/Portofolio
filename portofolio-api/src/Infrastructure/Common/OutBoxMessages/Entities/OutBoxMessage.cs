namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

public sealed class OutBoxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; }
    public string Content { get; init; }
    public System.DateTime CreatedOn { get; init; }
    public System.DateTime? ProcessedOn { get; private set; }
    
    public void Processed(System.DateTime processedOn)
    {
        ProcessedOn = processedOn;
    }
}