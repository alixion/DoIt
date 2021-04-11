using System;
using System.Collections.Generic;
using DoIt.Application.Common;
using DoIt.Domain;

namespace DoIt.Application.ToDos
{
    public class ToDoListDto:IMapFrom<ToDoList>
    {
        public ToDoListDto()
        {
            ToDoItems = new List<ToDoItemDto>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<ToDoItemDto> ToDoItems { get; set; }
    }
}