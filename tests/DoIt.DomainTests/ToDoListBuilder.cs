using DoIt.Domain;

namespace DoIt.DomainTests
{
    public class ToDoListBuilder
    {
        private ToDoList _toDoList;

        public ToDoListBuilder Title(string title)
        {
            _toDoList.ChangeTitle(title);
            return this;
        }


        public ToDoListBuilder WithDefaultValues()
        {
            _toDoList = new ToDoList("Test List");
            return this;
        }

        public ToDoListBuilder WithOneItem()
        {
            _toDoList.AddToDo("Test Item");
            return this;
        }

        public ToDoList Build() => _toDoList;
    }
}