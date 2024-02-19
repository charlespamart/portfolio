using System.Net;
using Application.Handlers.Todos.Commands.CreateTodo;
using Domain.Models;
using FastEndpoints;
using Mapster;
using MediatR;
using Presentation.FastEndpoints.Common;
using Presentation.FastEndpoints.V1.Endpoints.Todos.CreateTodo.Models;
using Presentation.FastEndpoints.V1.Endpoints.Todos.GetTodo;

namespace Presentation.FastEndpoints.V1.Endpoints.Todos.CreateTodo;

public sealed class CreateTodoEndpoint(ISender mediator)
    : Endpoint<CreateTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(1);
        Post(ApiRoutes.Todo.GetTodos);
        Description(setup =>
        {
            setup.Produces<Todo>((int)HttpStatusCode.Created);
            setup.Produces<ErrorResponse>((int)HttpStatusCode.BadRequest);
        });
    }

    public override async Task HandleAsync(CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(request.Adapt<CreateTodoCommand>(), cancellationToken);
        await SendCreatedAtAsync<GetTodoEndpoint>(todo.Id, todo, cancellation: cancellationToken);
    }
}