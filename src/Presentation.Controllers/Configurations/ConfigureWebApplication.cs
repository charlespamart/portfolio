namespace Presentation.Controllers.Configurations;

public static class ConfigureWebApplication
{
    public static WebApplication AddWebApplicationConfiguration(this WebApplication application)
    {
        application
            .UseSwagger()
            .UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                setup.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
            })
            .UseHttpsRedirection();
        return application;
    }
}