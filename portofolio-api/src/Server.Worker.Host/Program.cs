using SemSnel.Portofolio.Infrastructure;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Server.Application;
using Worker.Configs;

var builder = Host
    .CreateDefaultBuilder(args);

builder.ConfigureServices(
        (hostContext, services) =>
        {
            builder.ConfigureAppConfiguration(configureDelegate =>
            {
                configureDelegate
                    .AddConfigurationFiles(hostContext.HostingEnvironment);
            });

            var configuration = hostContext.Configuration;

            services
                .AddHostedService<SemSnel.Portofolio.Server.BackgroundServices.Worker>()
                .AddApplication(configuration)
                .AddInfrastructure(configuration);
        });

var host = builder
    .Build();

using var scope = host
    .Services
    .CreateScope();

var context = scope
    .ServiceProvider
    .GetRequiredService<IAppDatabaseContext>();

var initialiser = scope
    .ServiceProvider
    .GetRequiredService<IAppContextInitialiser>();

await initialiser
    .Initialise(context);

host.Run();