namespace SemSnel.Portofolio.Application.Users;

public sealed class FakeCurrentUser : ICurrentUser
{
    public Guid? Id { get; } = Guid.NewGuid();
    
    public string? Name { get; }
}