using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using DoIt.Domain.Common;

namespace DoIt.Domain
{
    public class ToDoList:Entity, IAggregateRoot
    {
        public ToDoList(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            _items = new List<ToDoItem>();
        }

        public Guid Id { get; }
        public string Title { get; private set; }

        private List<ToDoItem> _items;
        public IReadOnlyCollection<ToDoItem> Items => _items;


        public void ChangeTitle(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Title = title;
        }

        public Guid AddToDo(string toDoTitle)
        {
            Guard.Against.NullOrWhiteSpace(toDoTitle, nameof(toDoTitle));
            var todo = new ToDoItem(toDoTitle);
            _items.Add(todo);
            return todo.Id;
        }

        public void RemoveToDo(Guid toDoId)
        {
            _items.RemoveAll(x => x.Id == toDoId);
        }
    }
}
