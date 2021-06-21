using System;
using System.Threading.Tasks;
using DoIt.Application.ToDos;
using DoIt.ApplicationTests.Common;
using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;
using FluentAssertions;
using Xunit;

namespace DoIt.ApplicationTests.TodoLists
{
    [Collection(nameof(SliceFixture))]
    public class AddTodoListTests
    {
        private readonly SliceFixture _fixture;

        public AddTodoListTests(SliceFixture fixture)
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