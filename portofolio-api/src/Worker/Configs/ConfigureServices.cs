namespace Worker.Configs;

public static class ConfigureServices
{
    /// <summary>
    /// Add configuration files to the builder
    /// </summary>
    /// <param name="builder">builder</param>
    /// <param name="env">environment</param>
    /// <returns>builder</returns>
    public static IConfigurationBuilder AddConfigurationFiles(this IConfigurationBuilder builder, IHostEnvironment env)
    {
        var environmentName = env.EnvironmentName;
        
        builder
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder;
    }
}