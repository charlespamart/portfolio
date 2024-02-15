using Asp.Versioning;
using Domain.OptionConfigurations;
using Microsoft.OpenApi.Models;

namespace Presentation.Controllers.Configurations;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddOptionsConfigurations();
        services.AddSwagger();
        services.AddMvc();
        services.AddControllers();
        services.AddApiVersioning();

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

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
            setup.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });
        });
    }

    private static void AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.ReportApiVersions = true;
            setup.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}