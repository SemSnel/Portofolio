namespace SemSnel.Portofolio.Domain.Common.Entities;

/// <summary>
/// Interface for entities that are auditable.
/// </summary>
public interface IAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    bool IsDeleted { get; set; }
}
