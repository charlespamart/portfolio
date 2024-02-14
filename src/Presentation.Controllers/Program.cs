using Application;
using Infrastructure;
using Presentation.Controllers.Configurations;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services
    .AddPresentationServices()
    .AddApplicationServices()
    .AddInfrastructureServices();

var application = builder.Build();

await application.AddWebApplicationConfiguration().RunAsync();