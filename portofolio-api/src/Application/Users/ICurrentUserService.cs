namespace SemSnel.Portofolio.Application.Users;

public interface ICurrentUserService
{
    public Guid? Id { get; }
    public string? Name { get; }
    
    public bool IsAuthenticated()
    {
        return Id.HasValue;
    }
    
    public bool IsInRole(string role)
    {
        return false;
    }
    
    public bool HasPermission(string permission)
    {
        return false;
    }
    
    public bool HasClaim(string claimType, string claimValue)
    {
        return false;
    }

    bool HasRole(string role)
    {
        return false;
    }
}