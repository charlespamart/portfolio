using Application;
using Infrastructure;
using Presentation.FastEndpoints.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices()
    .AddApplicationServices()
    .AddInfrastructureServices();

await builder
    .Build()
    .AddWebApplicationConfiguration()
    .RunAsync();