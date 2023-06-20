using System;
using Application.Handlers.Todos.Queries.GetTodo;

namespace Presentation.Todos.GetTodo;

public sealed record GetTodoRequest()
{
    public Guid Id { get; init; }

    public GetTodoQuery ToApplication() => new() { Id = Id };
}