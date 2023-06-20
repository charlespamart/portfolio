using Application.Handlers.Todos.Commands.CreateTodo;

namespace Presentation.Todos.CreateTodo;

public sealed record CreateTodoRequest()
{
    public string Name { get; set; }
    public CreateTodoCommand ToApplication() => new() { Name = Name };
}