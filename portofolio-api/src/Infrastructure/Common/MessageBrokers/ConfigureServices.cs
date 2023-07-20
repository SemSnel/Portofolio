using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.DateTime;
using SemSnel.Portofolio.Infrastructure.Common.Files;
using SemSnel.Portofolio.Infrastructure.Common.Mapping;
using SemSnel.Portofolio.Infrastructure.Common.Mediatr;
using SemSnel.Portofolio.Infrastructure.Common.Persistence;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers;

public static class ConfigureServices
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IMessageService, MessageService>();
    }
}