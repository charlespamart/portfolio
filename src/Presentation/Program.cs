using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices()
    .AddApplicationServices()
    .AddInfrastructureServices();

await builder
    .Build()
    .AddWebApplicationConfiguration()
    .RunAsync();