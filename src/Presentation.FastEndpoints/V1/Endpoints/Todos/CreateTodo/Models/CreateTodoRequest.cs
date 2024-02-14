namespace Presentation.FastEndpoints.V1.Endpoints.Todos.CreateTodo.Models;

public sealed record CreateTodoRequest
{
    public required string Name { get; init; }
}