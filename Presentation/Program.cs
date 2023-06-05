using System;
using Application;
using CommunityToolkit.Diagnostics;
using Infrastructure;
using Kahdomi.Identity.Password;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Presentation;

[assembly: FunctionsStartup(typeof(Program))]

namespace Presentation;

public class Program : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var services = builder.Services;

        services.AddApplicationServices()
            .AddInfrastructureServices()
            .AddApplicationInsightsTelemetryWorkerService()
            .AddDistributedMemoryCache()
            .AddPasswordTokenManagement();
    }
}