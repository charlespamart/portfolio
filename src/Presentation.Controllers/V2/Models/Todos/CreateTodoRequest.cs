namespace Presentation.Controllers.V2.Models.Todos;

public sealed record CreateTodoRequest
{
    private readonly string _name = null!;
    public required string Name
    {
        get => _name;
        init => _name = value + "v2";
    }
}