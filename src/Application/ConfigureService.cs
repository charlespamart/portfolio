using System.Reflection;
using Application.Handlers.Todos.Commands.CreateTodo;
using Application.Validation;
using Domain.Models;
using FluentResults;
using FluentValidation;
using MediatR;
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
        services.AddMediatR(configuration =>
            configuration
                .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatRBehaviors());
    }

    private static void AddFluentValidation(this IServiceCollection services) =>
        services
            .AddTransient<IValidator<CreateTodoCommand>, CreateTodoCommandValidator>();

    private static void AddMediatRBehaviors(this MediatRServiceConfiguration configuration)
        => configuration.AddValidationBehavior<CreateTodoCommand, Todo>();
    
    private static void AddValidationBehavior<TRequest, TResponse>(
        this MediatRServiceConfiguration configuration)
        where TRequest : notnull
        => configuration
            .AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationBehavior<TRequest, TResponse>>();
}