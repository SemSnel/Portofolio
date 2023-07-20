namespace SemSnel.Portofolio.Application.Users;

public sealed class FakeUser : IUser
{
    public Guid Id { get; } = Guid.NewGuid();
}