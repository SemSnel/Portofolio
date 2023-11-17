using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Handlers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Specifications;
using SemSnel.Portofolio.Server.Application.Common.DateTime;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Consumers;

/// <summary>
/// A consumer for domain messages.
/// </summary>
/// <param name="handler"> The <see cref="IDomainMessageHandler"/>. </param>
/// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
/// <param name="unitOfWork"> The <see cref="IUnitOfWork"/>. </param>
/// <param name="context"> The <see cref="IAppDatabaseContext"/>. </param>
public sealed class DomainMessageConsumer(
        IDomainMessageHandler handler,
        IDateTimeProvider dateTimeProvider,
        ILogger<DomainMessageConsumer> logger,
        IUnitOfWork unitOfWork,
        IAppDatabaseContext context)
    : IDomainMessageConsumer
{
    private const int Take = 100;
    private const string QueueName = "outbox";

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Consume(CancellationToken cancellationToken = default)
    {
        var specification = new UnprocessedDomainMessagesSpecification();

        var messages = await context
            .DomainMessages
            .WithSpecification(specification)
            .ToListAsync(cancellationToken);

        logger.LogInformation("Consuming {Count} messages from queue {QueueName}.", messages.Count(), QueueName);

        foreach (var message in messages)
        {
            logger.LogInformation("Processing message {MessageId}.", message.Id);

            var result = await handler.Handle(message);

            if (result.IsError)
            {
                return result.FirstError;
            }

            message.Process(dateTimeProvider.Now());

            context.DomainMessages.Update(message);

            logger.LogInformation("Message {MessageId} processed.", message.Id);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success;
    }
}