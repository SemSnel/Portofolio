using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Application.Common.Authorisations;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthorisations()
            .AddUsers(configuration);
    }
}