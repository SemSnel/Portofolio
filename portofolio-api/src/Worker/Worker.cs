using MediatR;
using SemSnel.Portofolio.Application.Common.MessageBrokers;

namespace Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMessageRetriever _messageRetriever;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IMessageRetriever messageRetriever)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _messageRetriever = messageRetriever;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var messageService = scope.ServiceProvider.GetRequiredService<IMessageRetriever>();

            await RetrieveAndHandleMessages(stoppingToken, mediator, messageService);

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task RetrieveAndHandleMessages(
        CancellationToken stoppingToken,
        IMediator mediator,
        IMessageRetriever messageService)
    {
        var response = await _messageRetriever.Get("weather-forecasts-created", 10);

        if (response.IsError)
        {
            var error = response.FirstError;

            _logger.LogError("Error retrieving messages: {error}", error);

            return;
        }

        var messages = response.Value.ToArray();

        if (!messages.Any())
        {
            _logger.LogInformation("No messages found");

            await Task.Delay(1000, stoppingToken);

            return;
        }

        await HandleMessagesAsync(messages, mediator, messageService, stoppingToken);
    }

    private async Task HandleMessagesAsync(IEnumerable<IMessage> messages, IMediator mediator, IMessageRetriever messageRetriever, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Received {amount} messages", messages.Count());

        foreach (var message in messages)
        {
            _logger.LogInformation("Message: {message}", message);

            await HandleMessageAsync(message, mediator, messageRetriever, cancellationToken);
        }
    }
    
    private async Task HandleMessageAsync(IMessage message, IMediator mediator, IMessageRetriever messageRetriever, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Publish(message, cancellationToken);
            
            await messageRetriever.Delete(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing message: {message}", message);
            throw;
        }
    }
}