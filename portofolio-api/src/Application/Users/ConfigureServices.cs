using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SemSnel.Portofolio.Application.Users;

public static class ConfigureServices
{
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<ICurrentUser, FakeCurrentUser>();
    }
}