using Application.Handlers.Todos.Queries.GetTodos;

namespace Presentation.Todos.GetTodos;

public sealed record GetTodosRequest()
{
    public GetTodosQuery ToApplication() => new();
}