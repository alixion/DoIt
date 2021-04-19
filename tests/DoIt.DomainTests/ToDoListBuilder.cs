using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;

namespace DoIt.DomainTests
{
    public class ToDoListBuilder
    {
        private TodoList _todoList;

        public ToDoListBuilder Title(string title)
        {
            _todoList.ChangeTitle(title);
            return this;
        }


        public ToDoListBuilder WithDefaultValues()
        {
            _todoList = new TodoList("Test List");
            return this;
        }

        public ToDoListBuilder WithOneItem()
        {
            _todoList.AddToDo("Test Item");
            return this;
        }

        public TodoList Build() => _todoList;
    }
}