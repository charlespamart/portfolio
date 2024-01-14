using Domain.OptionConfigurations;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.AF;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services) =>
        services.AddOptionsConfigurations();

    private static IServiceCollection AddOptionsConfigurations(this IServiceCollection services)
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

        return services;
    }
}