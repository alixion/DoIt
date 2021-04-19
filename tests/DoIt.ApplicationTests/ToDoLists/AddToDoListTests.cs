using System;
using System.Threading.Tasks;
using DoIt.Application.ToDos;
using DoIt.ApplicationTests.Common;
using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;
using FluentAssertions;
using Xunit;

namespace DoIt.ApplicationTests.ToDoLists
{
    [Collection(nameof(SliceFixture))]
    public class AddToDoListTests
    {
        private readonly SliceFixture _fixture;

        public AddToDoListTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task ShouldAddNewToDoList()
        {
            var command = new AddTodoListCommand("Test");
            var result = await _fixture.SendAsync(command);

            result.Id.Should().NotBeEmpty();

            var list = await _fixture.FindAsync<TodoItem, Guid>(result.Id);
            list.Title.Should().Be(command.Title);
        }
        
    }
}