using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using DoIt.Domain.Common;

namespace DoIt.Domain.TodoListAggregate
{
    public class TodoList:AuditableEntity, IAggregateRoot
    {
        public TodoList(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(Title));
            Id = Guid.NewGuid();
            Title = title;
            _items = new List<TodoItem>();
        }

        public Guid Id { get; }
        public string Title { get; private set; }

        private List<TodoItem> _items;
        public IReadOnlyCollection<TodoItem> Items => _items;


        public void ChangeTitle(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Title = title;
        }

        public Guid AddToDo(string toDoTitle)
        {
            Guard.Against.NullOrWhiteSpace(toDoTitle, nameof(toDoTitle));
            var todo = new TodoItem(toDoTitle);
            _items.Add(todo);
            return todo.Id;
        }

        public void RemoveToDo(Guid toDoId)
        {
            _items.RemoveAll(x => x.Id == toDoId);
        }
    }
}
