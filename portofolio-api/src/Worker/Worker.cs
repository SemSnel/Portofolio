using MediatR;
using SemSnel.Portofolio.Application.Common.MessageBrokers;

namespace Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

            var response = await ReceiveMessagesAsync(mediator);

            if (response.Messages.Any())
            {
                await ProcessMessagesAsync(response.Messages, mediator, messageService);
            }
            else
            {
                _logger.LogInformation("No messages received");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task<ReceiveMessagesResponse> ReceiveMessagesAsync(IMediator mediator)
    {
        var errorOr = await mediator.Send(new ReceiveMessagesRequest());

        if (errorOr.IsError)
        {
            _logger.LogError("Error receiving messages: {error}", errorOr.FirstError.Description);
        }

        return errorOr.Value;
    }

    private async Task ProcessMessagesAsync(IEnumerable<IMessage> messages, IMediator mediator, IMessageService messageService)
    {
        _logger.LogInformation("Received {amount} messages", messages.Count());

        foreach (var message in messages)
        {
            _logger.LogInformation("Message: {message}", message);

            try
            {
                await mediator.Publish(message);
                _logger.LogInformation("Message processed: {message}", message);

                await messageService.Delete(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing message: {message}", message);
                throw;
            }
        }
    }
}