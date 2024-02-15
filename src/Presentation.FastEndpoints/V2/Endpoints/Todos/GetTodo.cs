using Application.Handlers.Todos.Queries.GetTodo;
using Domain.Models;
using FastEndpoints;
using MediatR;
using Presentation.FastEndpoints.Common;
using Presentation.FastEndpoints.V2.Models.Todos;

namespace Presentation.FastEndpoints.V2.Endpoints.Todos;

public sealed class GetTodo(ISender mediator)
    : Endpoint<GetTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(2);
        Get(ApiRoutes.Todo.GetTodo);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = request.TodoId }, cancellationToken);

        if (todo is null)
            await SendNotFoundAsync(cancellationToken);
        await SendOkAsync(todo!, cancellationToken);
    }
}