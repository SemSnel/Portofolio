using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

public sealed class OutBoxMessagePublisher : IOutboxMessagePublisher
{
    private readonly IOutBoxMessageRepository _repository;

    public OutBoxMessagePublisher(IOutBoxMessageRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Success>> Publish(OutBoxMessage message)
    {
        _repository.Add(message);
        
        return Result.Success();
    }

    public async Task<ErrorOr<Success>> Publish(IEnumerable<OutBoxMessage> messages)
    {
        _repository.AddRange(messages);
        
        return Result.Success();
    }
}