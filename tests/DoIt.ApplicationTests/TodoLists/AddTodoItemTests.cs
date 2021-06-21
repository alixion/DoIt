using System;
using System.Threading.Tasks;
using DoIt.Application.ToDos;
using DoIt.ApplicationTests.Common;
using DoIt.Domain.TodoListAggregate;
using FluentAssertions;
using Xunit;

namespace DoIt.ApplicationTests.TodoLists
{
    [Collection(nameof(SliceFixture))]
    public class AddTodoItemTests
    {
        private readonly SliceFixture _fixture;

        public AddTodoItemTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldAddItemToList()
        {
            var list = new TodoList("My list");
            await _fixture.AddAsync(list);
            
            var command = new AddTodoItemCommand(list.Id,"Do this");

            var result = await _fixture.SendAsync(command);
            result.Id.Should().NotBeEmpty();

            var item = await _fixture.FindAsync<TodoItem, Guid>(result.Id);
            item.Title.Should().Be(command.Title);
            item.TodoListId.Should().Be(list.Id);
        }
    }
}