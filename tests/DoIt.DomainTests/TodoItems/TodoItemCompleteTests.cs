using FluentAssertions;
using Xunit;

namespace DoIt.DomainTests.TodoItems
{
    public class TodoItemCompleteTests
    {
        [Fact]
        public void MarksItemAsDone()
        {
            var todo = new TodoItemBuilder()
                .WithDefaultValues()
                .Build();

            todo.MarkDone();
            todo.Done.Should().BeTrue();
        }

        [Fact]
        public void MarksItemAsNotDone()
        {
            var todo = new TodoItemBuilder()
                .WithDefaultValues()
                .Done()
                .Build();

            todo.MarkUndone();
            todo.Done.Should().BeFalse();
        }

    }
}