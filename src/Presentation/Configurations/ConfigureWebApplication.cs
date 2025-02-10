using Carter;
using Microsoft.AspNetCore.Builder;

namespace Presentation.Configurations;

public static class ConfigureWebApplication
{
    public static WebApplication AddWebApplicationConfiguration(this WebApplication application)
    {
        application
            .UseHttpsRedirection()
            .UseExceptionHandler();

        application.MapCarter();

        return application;
    }
}