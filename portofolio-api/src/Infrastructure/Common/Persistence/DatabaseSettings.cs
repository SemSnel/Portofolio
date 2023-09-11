using System.ComponentModel.DataAnnotations;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

public class DatabaseSettings : IValidatableObject
{
    public const string Section = "DatabaseSettings";
    
    public string ConnectionString { get; init; } = string.Empty;
    
    public string Provider { get; init; } = string.Empty;

    public static readonly IEnumerable<string> Providers = new[]
    {
        "SqlServer", 
        "Sqlite",
        "MySql",
        "InMemory"
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            yield return new ValidationResult("ConnectionString is required", new[] { nameof(ConnectionString) });
        }
        
        if (string.IsNullOrWhiteSpace(Provider))
        {
            yield return new ValidationResult("Provider is required", new[] { nameof(Provider) });
        }
        
        if (!Providers.Contains(Provider))
        {
            yield return new ValidationResult($"Provider must be one of the following: {string.Join(", ", Providers)}", new[] { nameof(Provider) });
        }
    }
}