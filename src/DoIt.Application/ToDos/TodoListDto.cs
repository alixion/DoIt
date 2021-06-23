using System;
using System.Collections.Generic;
using DoIt.Application.Common;
using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;

namespace DoIt.Application.ToDos
{
    public class TodoListDto:IMapFrom<TodoList>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IReadOnlyCollection<TodoItemDto> Items { get; set; }
    }
}