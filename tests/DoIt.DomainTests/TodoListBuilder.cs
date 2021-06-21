using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;

namespace DoIt.DomainTests
{
    public class TodoListBuilder
    {
        private TodoList _todoList;

        public TodoListBuilder Title(string title)
        {
            _todoList.ChangeTitle(title);
            return this;
        }


        public TodoListBuilder WithDefaultValues()
        {
            _todoList = new TodoList("Test List");
            return this;
        }

        public TodoListBuilder WithOneItem()
        {
            _todoList.AddToDo("Test Item");
            return this;
        }

        public TodoList Build() => _todoList;
    }
}