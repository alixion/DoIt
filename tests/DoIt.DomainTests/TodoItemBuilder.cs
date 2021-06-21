using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;

namespace DoIt.DomainTests
{
    public class TodoItemBuilder
    {
        private TodoItem _todo;

        public TodoItemBuilder Title(string title)
        {
            _todo.ChangeTitle(title);
            return this;
        }

        public TodoItemBuilder Note(string note)
        {
            _todo.SetNote(note);
            return this;
        }

        public TodoItemBuilder Done()
        {
            _todo.MarkDone();
            return this;
        }

        public TodoItemBuilder Notdone()
        {
            _todo.MarkUndone();
            return this;
        }

        public TodoItemBuilder WithDefaultValues()
        {
            _todo = new TodoItem("Test Item");
            return this;
        }

        public TodoItem Build() => _todo;
    }
}