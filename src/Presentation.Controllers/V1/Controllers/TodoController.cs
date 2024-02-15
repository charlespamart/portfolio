using System.Net.Mime;
using Application.Handlers.Todos.Commands.CreateTodo;
using Application.Handlers.Todos.Queries.GetTodo;
using Application.Handlers.Todos.Queries.GetTodos;
using Asp.Versioning;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.V1.Models.Todos;
using Presentation.FastEndpoints.Common;

namespace Presentation.Controllers.V1.Controllers;

[ApiController]
[ApiVersion("1.0")]
public sealed class TodoController(
    ISender mediator)
    : ControllerBase
{
    private const string ControllerName = "todos";

    [HttpGet(ApiRoutes.Todo.GetTodo)]
    [Produces<Todo>]
    [ActionName(nameof(GetTodoAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<Todo>> GetTodoAsync(
        [FromRoute] Guid todoId,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = todoId }, cancellationToken);

        if (todo is null)
            return NotFound();

        return todo;
    }

    [HttpGet(ApiRoutes.Todo.GetTodos)]
    [Produces<ICollection<Todo>>]
    [ActionName(nameof(GetTodosAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<ICollection<Todo>>> GetTodosAsync(
        CancellationToken cancellationToken) =>
        (await mediator.Send(new GetTodosQuery(), cancellationToken)).ToList();

    [HttpPost(ApiRoutes.Todo.CreateTodo)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces<Todo>]
    [ActionName(nameof(CreateTodoAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<Todo>> CreateTodoAsync(
        [FromBody] CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(request.Adapt<CreateTodoCommand>(), cancellationToken);
        return CreatedAtAction(nameof(CreateTodoAsync), new { id = todo.Id }, todo);
    }
}