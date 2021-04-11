using DoIt.DomainTests;
using FluentAssertions;
using Xunit;

namespace DoIt.Domain.Tests.ToDoItems
{
    public class ToDoItemCompleteTests
    {
        [Fact]
        public void MarksItemAsDone()
        {
            var todo = new ToDoItemBuilder()
                .WithDefaultValues()
                .Build();

            todo.MarkDone();
            todo.Done.Should().BeTrue();


        }

        [Fact]
        public void MarksItemAsNotdone()
        {
            var todo = new ToDoItemBuilder()
                .WithDefaultValues()
                .Done()
                .Build();

            todo.MarkUndone();
            todo.Done.Should().BeFalse();
        }

    }
}