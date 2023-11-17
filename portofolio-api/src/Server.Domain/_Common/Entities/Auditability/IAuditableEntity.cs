namespace SemSnel.Portofolio.Domain._Common.Entities.Auditability;

/// <summary>
/// Interface for entities that are auditable.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the created on.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the last modified by.
    /// </summary>
    public Guid? LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the last modified on.
    /// </summary>
    public DateTime? LastModifiedOn { get; set; }

    /// <summary>
    /// Gets or sets the deleted by.
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets or sets the deleted on.
    /// </summary>
    public DateTime? DeletedOn { get; set; }
}
