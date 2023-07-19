using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.Identity.Resources;

public interface IIdentityResource : IAuditableEntity
{
    public Guid OwnerId { get; set; }
    public IEnumerable<Guid> Users { get; set; }
}