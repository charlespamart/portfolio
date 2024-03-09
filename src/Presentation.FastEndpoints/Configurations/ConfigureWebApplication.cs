using FastEndpoints;
using FastEndpoints.Swagger;

namespace Presentation.FastEndpoints.Configurations;

public static class ConfigureWebApplication
{
    public static WebApplication AddWebApplicationConfiguration(this WebApplication application)
    {
        application
            .UseHttpsRedirection()
            .UseFastEndpoints(setup =>
            {
                setup.Versioning.Prefix = "v";
                setup.Versioning.PrependToRoute = true;
                setup.Versioning.DefaultVersion = 1;
                setup.Endpoints.RoutePrefix = "api";
                setup.Endpoints.Configurator = ep =>
                {
                    ep.AllowAnonymous();
                };
            })
            .UseSwaggerGen();
        return application;
    }
}