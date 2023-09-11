using Host.Configs;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application;
using SemSnel.Portofolio.Infrastructure;
using SemSnel.Portofolio.Infrastructure.Common.Authentication;
using SemSnel.Portofolio.Infrastructure.Common.Authorization;
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
    .AddInfrastructure(configuration)
    .AddAuthenticationServices(configuration)
    .AddAuthorizationServices(configuration);

services
        .AddSwaggerGen();

var app = builder.Build();

app
    .UseServer();

// make scope to dispose of database context

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<IAppDatabaseContext>();

var initialiser = scope.ServiceProvider.GetRequiredService<IAppContextInitialiser>();

await initialiser
    .Initialise(context);

scope.Dispose();

app.Run();