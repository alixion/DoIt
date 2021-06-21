using System;
using Ardalis.GuardClauses;
using DoIt.Domain.Common;

namespace DoIt.Domain.TodoListAggregate
{
    public class TodoItem:AuditableEntity
    {
        public TodoItem(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(Title));
            
            Id = Guid.NewGuid();
            Title = title;
            Done = false;
            Note = default;
        }

        public Guid Id { get; }
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public string? Note { get; private set; }

        public Guid TodoListId { get; set; }
        
        
        public void SetNote(string note)
        {
            Note = note;
        }

        public void ChangeTitle(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Title = title;
        }

        public void MarkDone()
        {
            Done = true;
        }

        public void MarkUndone()
        {
            Done = false;
        }
        
    }
    
    
    
}