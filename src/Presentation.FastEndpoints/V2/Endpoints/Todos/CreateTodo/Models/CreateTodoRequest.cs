namespace Presentation.FastEndpoints.V2.Endpoints.Todos.CreateTodo.Models;

public sealed record CreateTodoRequest
{
    private readonly string _name = null!;

    public required string Name
    {
        get => _name;
        init => _name = value + "v2";
    }
}