using Application;
using Infrastructure;
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
        var configuration = builder.GetContext().Configuration;

        services
            .AddPresentationServices(configuration)
            .AddApplicationServices()
            .AddInfrastructureServices()
            .AddApplicationInsightsTelemetryWorkerService();
    }
}