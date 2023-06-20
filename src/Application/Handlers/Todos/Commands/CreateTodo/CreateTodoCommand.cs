using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand : IRequest<Todo>
{
    public string Name { get; init; } = null!;
}

public sealed record
    CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo>
{
    private readonly ITodoDbContext _todoDbContext;

    public CreateTodoCommandHandler(ITodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task<Todo> Handle(CreateTodoCommand request,
        CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Name = request.Name
        };
        _todoDbContext.Todo.Add(todo);

        await _todoDbContext.SaveChangesAsync(cancellationToken);

        return todo;
    }
}