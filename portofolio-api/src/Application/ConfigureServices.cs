

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SemSnel.Portofolio.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}