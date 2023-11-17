using System.ComponentModel.DataAnnotations;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

public class IdempotencySettings : IValidatableObject
{
    public const string SectionName = "IdempotencySettings";
    
    public string HeaderName { get; init; } = "Idempotency-Key";
    
    public int ExpirationInterval { get; init; } = 30;
    
    public string ExpirationIntervalUnit { get; init; } = "days";
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(HeaderName))
        {
            yield return new ValidationResult("HeaderName is required", new[] { nameof(HeaderName) });
        }
        
        if (ExpirationInterval <= 0)
        {
            yield return new ValidationResult("Expiration must be greater than zero", new[] { nameof(ExpirationInterval) });
        }
        
        if (string.IsNullOrEmpty(ExpirationIntervalUnit))
        {
            yield return new ValidationResult("ExpirationIntervalUnit is required", new[] { nameof(ExpirationIntervalUnit) });
        }
        
        if (!string.IsNullOrEmpty(ExpirationIntervalUnit) 
            && !ExpirationIntervalUnit.Equals("seconds", StringComparison.OrdinalIgnoreCase) 
            && !ExpirationIntervalUnit.Equals("minutes", StringComparison.OrdinalIgnoreCase)
            && !ExpirationIntervalUnit.Equals("hours", StringComparison.OrdinalIgnoreCase)
            && !ExpirationIntervalUnit.Equals("days", StringComparison.OrdinalIgnoreCase)
            )
        {
            yield return new ValidationResult("ExpirationIntervalUnit must be seconds, minutes, hours or days", 
                new[] { nameof(ExpirationIntervalUnit) });
        }
    }
}