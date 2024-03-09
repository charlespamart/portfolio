using Application;
using Infrastructure;
using Presentation.Controllers.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices()
    .AddApplicationServices()
    .AddInfrastructureServices();

var application = builder.Build();

await application
    .AddWebApplicationConfiguration()
    .RunAsync();