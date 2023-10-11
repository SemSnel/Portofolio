namespace SemSnel.Portofolio.Application.Users;

public sealed class FakeCurrentUserService : ICurrentUserService
{
    public Guid? Id { get; } = Guid.NewGuid();
    
    public string? Name { get; }
}