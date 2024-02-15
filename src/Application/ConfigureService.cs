using Application.Handlers.Todos.Commands.CreateTodo;
using Application.Handlers.Todos.Queries.GetTodo;
using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddFluentValidation();

        return services;
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        var serviceConfig = new MediatRServiceConfiguration();
        ServiceRegistrar.AddRequiredServices(services, serviceConfig);

        services
            .AddTransient<IRequestHandler<GetTodosQuery, ICollection<Todo>>, GetTodosQueryHandler>()
            .AddTransient<IRequestHandler<GetTodoQuery, Todo?>, GetTodoQueryHandler>()
            .AddTransient<IRequestHandler<CreateTodoCommand, Todo>, CreateTodoCommandHandler>();
    }

    private static void AddFluentValidation(this IServiceCollection services) =>
        services
            .AddSingleton<IValidator<CreateTodoCommand>, CreateTodoCommandValidator>()
            .AddSingleton<IValidator<GetTodoQuery>, GetTodoQueryValidator>();
}