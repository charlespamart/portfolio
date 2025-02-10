using Application.Common.Interfaces;
using Application.Handlers.Todos.Commands.CreateTodo;
using AutoFixture;
using Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Application.Tests.Handlers.Todos.Commands.CreateTodo;

public sealed class CreateTodoShould
{
    private readonly CreateTodoCommandHandler _handler;
    private readonly ITodoDbContext _todoDbContext;
    private readonly Fixture _fixture = new();

    public CreateTodoShould()
    {
        _todoDbContext = Substitute.For<ITodoDbContext>();

        _handler = new CreateTodoCommandHandler(_todoDbContext);
    }

    [Fact]
    public async Task ReturnTodo_WhenCommand_IsValid()
    {
        var command = _fixture.Create<CreateTodoCommand>();
        var expected = new Todo
        {
            Name = command.Name,
            Index = 1
        };
        // TODO : Mock DBContext for testing

        var sut = await _handler.Handle(command, CancellationToken.None);

        sut.Should().Be(expected);
    }
}