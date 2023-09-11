using FluentValidation;
using SemSnel.Portofolio.Application.Common.Validations;
using SemSnel.Portofolio.Application.Users;

namespace SemSnel.Portofolio.Infrastructure.Common.Validations;

public static class ConfigureServices
{
    public static IServiceCollection AddValidationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[]
        {
            typeof(ConfigureServices).Assembly,
            typeof(ValidationBehaviour<,>).Assembly
        };
        
        return services
            .AddValidatorsFromAssemblies(assemblies);
    }
}