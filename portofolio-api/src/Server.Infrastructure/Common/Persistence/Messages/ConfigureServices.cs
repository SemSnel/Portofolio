using System.Reflection;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configures the message context for the specified assemblies.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the message context to.</param>
    /// <param name="assemblies"> The assemblies to scan for <see cref="IMessage"/> implementations.</param>
    /// <returns> The <see cref="IServiceCollection"/>. </returns>
    public static IServiceCollection AddMessageContext(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddTransient<IMessagesDatabaseContext>(provider => provider.GetRequiredService<IAppDatabaseContext>());

        return services;
    }
}