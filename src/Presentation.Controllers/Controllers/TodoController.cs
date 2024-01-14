using System.Net.Mime;
using Application.Handlers.Todos.Commands.CreateTodo;
using Application.Handlers.Todos.Queries.GetTodo;
using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Models.Todos;

namespace Presentation.Controllers.Controllers;

[ApiController]
[Route($"api/{ControllerName}")]
public sealed class TodoController(
    ISender mediator)
    : ControllerBase
{
    private const string ControllerName = "todos";

    [HttpGet("{todoId:guid}")]
    [Produces<Todo>]
    [ActionName(nameof(GetTodoAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> GetTodoAsync(
        [FromRoute] Guid todoId,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = todoId }, cancellationToken);

        if (todo is null)
            return NotFound();

        return todo;
    }

    [HttpGet]
    [Produces<Todo>]
    [ActionName(nameof(GetTodos))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<Todo>>> GetTodos(
        CancellationToken cancellationToken) =>
        (await mediator.Send(new GetTodosQuery(), cancellationToken)).ToList();

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces<Todo>]
    [ActionName(nameof(CreateTodo))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Todo>> CreateTodo(
        [FromBody] CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(request.Adapt<CreateTodoCommand>(), cancellationToken);
        return CreatedAtAction(nameof(CreateTodo), new { id = todo.Id }, todo);
    }
}