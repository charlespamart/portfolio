using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Todos.CreateTodo;
using Presentation.Todos.GetTodo;

namespace Presentation;

public static class ConfigureService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services) =>
        services
            .AddFluentValidation();

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        => services
            .AddSingleton<IValidator<GetTodoRequest>, GetTodoRequestValidator>()
            .AddSingleton<IValidator<CreateTodoRequest>, CreateTodoRequestValidator>();
}