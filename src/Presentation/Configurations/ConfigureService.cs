using Asp.Versioning;
using Carter;
using Domain.OptionConfigurations;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Middleware;

namespace Presentation.Configurations;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddOptionsConfigurations();
        services.AddGlobalExceptionHandlerMiddleware();
        services.AddApiVersioning();
        services.AddCarter();

        return services;
    }

    private static void AddOptionsConfigurations(this IServiceCollection services)
    {
        services
            .AddOptions<PostgresDbOptions>()
            .BindConfiguration("PostgresDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddGlobalExceptionHandlerMiddleware(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }

    private static void AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
}