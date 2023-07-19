using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.Identity.Roles;

public sealed class IdentityRole : Entity<Guid>, IAuditableEntity
{
    public string Name { get; set; } = default!;
    public string NormalizedName { get; set; } = default!;
    public string ConcurrencyStamp { get; set; } = default!;
    public string? Description { get; set; }

    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; } = false;
}