namespace SemSnel.Portofolio.Application.Users;

public interface IUser
{
    public Guid Id { get; }
}

public sealed class FakeUser : IUser
{
    public Guid Id { get; } = Guid.NewGuid();
}