using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;
using SemSnel.Portofolio.Infrastructure.Common.OutBoxMessages.PubSub;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

public sealed class OutBoxMessageConsumer : IOutBoxMessageConsumer
{
    private const int Take = 100;
    private const string QueueName = "outbox";
    
    private readonly IOutBoxMessageRepository _repository;
    private readonly IOutboxMessageHandler _handler;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<OutBoxMessageConsumer> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OutBoxMessageConsumer(IOutBoxMessageRepository repository, IOutboxMessageHandler handler, IDateTimeProvider dateTimeProvider, ILogger<OutBoxMessageConsumer> logger, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _handler = handler;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Consume(CancellationToken cancellationToken = default)
    {
        var errorOr = await _repository.Get(QueueName, Take);

        if (errorOr.IsError)
        {
            return errorOr.FirstError;
        }
        
        var messages = errorOr.Value;
        
        _logger.LogInformation("Consuming {Count} messages from queue {QueueName}.", messages.Count(), QueueName);
        
        foreach (var message in messages)
        {
            _logger.LogInformation("Processing message {MessageId}.", message.Id);
            
            var result = await _handler.Handle(message);
            
            if (result.IsError)
            {
                return result.FirstError;
            }

            message.Processed(_dateTimeProvider.Now());
            
            _repository.Update(message);
            
            _logger.LogInformation("Message {MessageId} processed.", message.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        return Result.Success();
    }
}