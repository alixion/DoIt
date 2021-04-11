using DoIt.Domain;

namespace DoIt.DomainTests
{
    public class ToDoItemBuilder
    {
        private ToDoItem _todo;

        public ToDoItemBuilder Title(string title)
        {
            _todo.ChangeTitle(title);
            return this;
        }

        public ToDoItemBuilder Note(string note)
        {
            _todo.SetNote(note);
            return this;
        }

        public ToDoItemBuilder Done()
        {
            _todo.MarkDone();
            return this;
        }

        public ToDoItemBuilder Notdone()
        {
            _todo.MarkUndone();
            return this;
        }

        public ToDoItemBuilder WithDefaultValues()
        {
            _todo = new ToDoItem("Test Item");
            return this;
        }

        public ToDoItem Build() => _todo;
    }
}