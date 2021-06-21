using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DoIt.DomainTests.TodoLists
{
    public class ListItemsTests
    {
        
        [Fact]
        public void CanAddItemsToList()
        {
            var list = new TodoListBuilder()
                .WithDefaultValues()
                .Build();

            list.Items.Should().BeEmpty();

            list.AddToDo("Test item");

            list.Items.Should().HaveCount(1);
        }

        [Fact]
        public void CanRemoveItemsFromList()
        {
            var list = new TodoListBuilder()
                .WithDefaultValues()
                .WithOneItem()
                .Build();

            var firstItem = list.Items.First();

            list.RemoveToDo(firstItem.Id);

            list.Items.Should().BeEmpty();
        }
    }
}
