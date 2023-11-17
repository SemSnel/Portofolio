namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

public record IdempotentRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public System.DateTime CreatedOn { get; init; }
    
    public string RequestId { get; set; } = default!;
    
    public string? RequestBody { get; set; } = default!;
    
    public string? RequestHeaders { get; set; } = default!;
    
    public string? ResponseBody { get; set; } = default!;
    
    public int ResponseStatusCode { get; set; } = default!;
}