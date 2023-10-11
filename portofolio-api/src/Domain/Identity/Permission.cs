using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.ValueObjects;

namespace SemSnel.Portofolio.Domain.Identity;

public sealed class Permission : ValueObject
{
    public string Name { get; }

    private Permission(string name)
    {
        Name = name;
    }
    
    public static ErrorOr<Permission> Create(string name)
    {
        return new Permission(name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
    
    public static implicit operator string(Permission permission)
    {
        return permission.Name;
    }
    
    public static implicit operator Permission(string permission)
    {
        return new Permission(permission);
    }
}