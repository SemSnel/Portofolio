using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

public sealed class OutBoxMessageConsumer : IOutBoxMessageConsumer
{
    private const int Take = 100;
    private const string QueueName = "outbox";
    
    private readonly IOutBoxMessageRepository _repository;
    private readonly IOutboxMessageHandler _handler;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OutBoxMessageConsumer(IOutBoxMessageRepository repository, IOutboxMessageHandler handler, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _handler = handler;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Success>> Consume(CancellationToken cancellationToken = default)
    {
        var errorOr = await _repository.Get(QueueName, Take);

        if (errorOr.IsError)
        {
            return errorOr.FirstError;
        }
        
        var messages = errorOr.Value;
        
        foreach (var message in messages)
        {
            var result = await _handler.Handle(message);
            
            if (result.IsError)
            {
                return result.FirstError;
            }

            message.Processed(_dateTimeProvider.Now());
            
            _repository.Update(message);
        }
        
        return Result.Success();
    }
}