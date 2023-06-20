using Domain.OptionConfigurations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Todos.CreateTodo;
using Presentation.Todos.GetTodo;

namespace Presentation;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddOptionsConfigurations(configuration)
            .AddFluentValidation();

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        => services
            .AddSingleton<IValidator<GetTodoRequest>, GetTodoRequestValidator>()
            .AddSingleton<IValidator<CreateTodoRequest>, CreateTodoRequestValidator>();

    private static IServiceCollection AddOptionsConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        BlobStorageOptions blobStorageOptions = null!;
        services.ConfigureAndValidate<BlobStorageOptions>(blobStorageOptions =>
        {
            blobStorageOptions = configuration.GetSection("BlobStorage").Get<BlobStorageOptions>();
            configuration.GetSection("BlobStorage").Bind(blobStorageOptions);
        });

        // services.AddOptions<BlobStorageOptions>().Configure<IConfiguration>((storageConfiguration, configuration) =>
        // {
        //     configuration.GetSection("BlobStorage").Bind(storageConfiguration);
        // }).ValidateDataAnnotationsRecursively().ValidateOnStart();

        return services;
    }
}