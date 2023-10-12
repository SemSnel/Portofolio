using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.DateTime;
using SemSnel.Portofolio.Infrastructure.Common.Files;
using SemSnel.Portofolio.Infrastructure.Common.Mapping;
using SemSnel.Portofolio.Infrastructure.Common.Mediatr;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Factories;
using SemSnel.Portofolio.Infrastructure.Common.OutBoxMessages.PubSub;
using SemSnel.Portofolio.Infrastructure.Common.Persistence;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers;

public static class ConfigureServices
{
    public static IServiceCollection AddOutBoxMessages(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<OutBoxMessageTypeScanner>()
            .AddTransient<IOutboxMessageFactory, OutboxMessageFactory>()
            .AddTransient<IOutboxMessagePublisher, OutBoxMessagePublisher>()
            .AddTransient<IOutBoxMessageConsumer, OutBoxMessageConsumer>()
            .AddTransient<IOutboxMessageHandler, OutboxMessageHandler>()
            .AddTransient<IOutBoxMessageRepository, OutBoxMessageRepository>()
            .AddSingleton<IMessageTypeDictionary>(
                provider =>
                {
                    var scanner = provider
                        .GetRequiredService<OutBoxMessageTypeScanner>();
                    
                    var assembly = typeof(WeatherForecast).Assembly;
                    
                    var dictionary = scanner
                        .Scan(assembly);
                    
                    return new MessageTypeDictionary(dictionary);
                });
        
        return services;
    }
}