using Domain.OptionConfigurations;

namespace Presentation.Controllers;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddOptionsConfigurations()
            .AddSwagger();
        services.AddMvc();
        services.AddControllers();

        return services;
    }

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

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}