using Application.Handlers.Todos.Commands.CreateTodo;
using Domain.Models;
using FastEndpoints;
using Mapster;
using MediatR;
using Presentation.FastEndpoints.Common;
using Presentation.FastEndpoints.V1.Models.Todos;

namespace Presentation.FastEndpoints.V1.Endpoints.Todos;

public sealed class CreateTodo(ISender mediator)
    : Endpoint<CreateTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(1);
        Post(ApiRoutes.Todo.CreateTodo);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(request.Adapt<CreateTodoCommand>(), cancellationToken);
        await SendCreatedAtAsync<GetTodo>(todo.Id, todo, cancellation: cancellationToken);
    }
}