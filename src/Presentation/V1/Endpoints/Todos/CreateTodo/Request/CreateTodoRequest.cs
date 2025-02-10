namespace Presentation.V1.Endpoints.Todos.CreateTodo.Request;

public sealed class CreateTodoRequest
{
    public required string Name { get; init; }
    public required int? Index { get; init; }
}