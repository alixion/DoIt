using System;
using Ardalis.GuardClauses;

namespace DoIt.Domain
{
    public class ToDoItem
    {
        public ToDoItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            Done = false;
            Note = default(string);
        }

        public Guid Id { get; }
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public string Note { get; private set; }
        
        
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