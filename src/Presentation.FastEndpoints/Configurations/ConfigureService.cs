using Domain.OptionConfigurations;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Presentation.FastEndpoints.Configurations;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddOptionsConfigurations();
        services.AddSwaggerDocuments();
        services.AddFastEndpoints();

        return services;
    }

    private static void AddOptionsConfigurations(this IServiceCollection services)
    {
        services
            .AddOptions<PostgresDbOptions>()
            .BindConfiguration("PostgresDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<MySqlDbOptions>()
            .BindConfiguration("MySqlDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<InMemoryDbOptions>()
            .BindConfiguration("InMemoryDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddSwaggerDocuments(this IServiceCollection services)
    {
        services.SwaggerDocument(setup =>
            {
                setup.MaxEndpointVersion = 1;
                setup.DocumentSettings = s =>
                {
                    s.DocumentName = "V1";
                    s.Title = "API V1";
                    s.Version = "v1";
                };
            })
            .SwaggerDocument(setup =>
            {
                setup.MaxEndpointVersion = 2;
                setup.DocumentSettings = s =>
                {
                    s.DocumentName = "V2";
                    s.Title = "API V2";
                    s.Version = "v2";
                };
            });
    }
}