using Application;
using Infrastructure;
using Microsoft.Extensions.Hosting;
using Presentation.AF;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services
            .AddPresentationServices()
            .AddApplicationServices()
            .AddInfrastructureServices();
    })
    .Build();


await host.RunAsync();