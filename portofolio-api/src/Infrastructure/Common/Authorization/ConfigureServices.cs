namespace SemSnel.Portofolio.Infrastructure.Common.Authorization;

public static class ConfigureServices
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var builder = services.AddAuthorization();

        return services;
    }
    
    public static IApplicationBuilder UseAuthorizationServices(this IApplicationBuilder app)
    {
        app.UseAuthorization();

        return app;
    }
}