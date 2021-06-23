using System;
using System.Collections.Generic;
using DoIt.Application.Common;
using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;

namespace DoIt.Application.ToDos
{
    public class TodoItemDto:IMapFrom<TodoItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
        public Guid TodoListId { get; set; }
    }
}