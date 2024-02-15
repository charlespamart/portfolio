namespace Presentation.FastEndpoints.V1.Endpoints.Todos.GetTodo.Models;

public sealed class GetTodoRequest
{
    public Guid TodoId { get; init; }
}