using Host.Configs;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application;
using SemSnel.Portofolio.Infrastructure;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Server;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

configuration
    .AddConfigurationFiles(builder.Environment);

var services = builder.Services;

services
    .AddServer(configuration)
    .AddApplication(configuration)
    .AddInfrastructure(configuration);

services
        .AddSwaggerGen();

var app = builder.Build();

app
    .UseServer()
    .UseInfrastructure();


// make scope to dispose of database context

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<IAppDatabaseContext>();

var initialiser = scope.ServiceProvider.GetRequiredService<IAppContextInitialiser>();

await initialiser
    .Initialise(context);

var forecasts = await context
    .WeatherForecasts
    .ToListAsync();

forecasts.First();

scope.Dispose();

app.Run();