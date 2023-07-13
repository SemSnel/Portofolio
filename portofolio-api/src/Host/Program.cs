using System.Reflection;
using Host.Configs;
using SemSnel.Portofolio.Infrastructure;
using SemSnel.Portofolio.Server;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

configuration.AddConfigurationFiles(builder.Environment);

var services = builder.Services;

services
    .AddServer(configuration)
    .AddInfrastructure(configuration);



services
        .AddSwaggerGen();

var app = builder.Build();

app
    .UseServer()
    .UseInfrastructure();

app.Run();