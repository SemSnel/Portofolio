using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Consumers;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Factories;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Handlers;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Publishers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages;

/// <summary>
/// A static class for configuring the services for the messages.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the services for the messages.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    public static IServiceCollection AddDomainMessagesServices(this IServiceCollection services, IConfiguration configuration)
    {
        var messageAssembly = typeof(WeatherForecast).Assembly;

        services
            .AddTransient<IDomainMessageFactory, DomainMessageFactory>()
            .AddTransient<IDomainMessagePublisher, DomainMessagePublisher>()
            .AddTransient<IDomainMessageConsumer, DomainMessageConsumer>()
            .AddTransient<IDomainMessageHandler, DomainMessageHandler>()
            .AddDomainMessagesMappings(messageAssembly)
            .AddOutboxMessagesMappings(messageAssembly)
            .AddInboxMessagesMappings(messageAssembly);

        return services;
    }
}