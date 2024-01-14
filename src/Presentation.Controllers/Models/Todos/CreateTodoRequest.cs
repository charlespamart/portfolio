namespace Presentation.Controllers.Models.Todos;

public sealed record CreateTodoRequest
{
    public required string Name { get; init; }
}