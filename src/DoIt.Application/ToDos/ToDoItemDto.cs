using System;
using DoIt.Application.Common;
using DoIt.Domain;

namespace DoIt.Application.ToDos
{
    public class ToDoItemDto:IMapFrom<ToDoItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
    }
}