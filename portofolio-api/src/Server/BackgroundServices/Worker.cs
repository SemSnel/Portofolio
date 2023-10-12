using MediatR;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;
using SemSnel.Portofolio.Infrastructure.Common.OutBoxMessages.PubSub;

namespace Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", _dateTimeProvider.Now());

            var delayTask = Task.Delay(5000, stoppingToken);
            
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
            
                var consumer = scope
                    .ServiceProvider
                    .GetRequiredService<IOutBoxMessageConsumer>();
                
                var consumeTask = consumer.Consume(stoppingToken);
                
                await Task.WhenAll(consumeTask, delayTask);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error consuming messages.");
                
                await delayTask;
            }
        }
    }
}