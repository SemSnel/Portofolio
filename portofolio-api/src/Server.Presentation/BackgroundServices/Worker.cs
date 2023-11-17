using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Consumers;
using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Server.BackgroundServices;

/// <summary>
/// A worker for consuming messages.
/// </summary>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
/// <param name="serviceScopeFactory"> The <see cref="IServiceScopeFactory"/>. </param>
/// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
public class Worker(
        ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IDateTimeProvider dateTimeProvider)
    : BackgroundService
{
    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", dateTimeProvider.Now());

            var delayTask = Task.Delay(5000, stoppingToken);

            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                var consumer = scope
                    .ServiceProvider
                    .GetRequiredService<IDomainMessageConsumer>();

                var consumeTask = consumer.Consume(stoppingToken);

                await Task.WhenAll(consumeTask, delayTask);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error consuming messages.");

                await delayTask;
            }
        }
    }
}