namespace Presentation.AF.Todos.Models.Todos;

public sealed record CreateTodoRequest
{
    public required string Name { get; init; }
}