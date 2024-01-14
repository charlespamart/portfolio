using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand : IRequest<Todo>
{
    public required string Name { get; init; }
}

public sealed class
    CreateTodoCommandHandler(ITodoDbContext todoDbContext) : IRequestHandler<CreateTodoCommand, Todo>
{
    public async Task<Todo> Handle(CreateTodoCommand request,
        CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Name = request.Name
        };

        todoDbContext.Todo.Add(todo);

        await todoDbContext.SaveChangesAsync(cancellationToken);

        return todo;
    }
}