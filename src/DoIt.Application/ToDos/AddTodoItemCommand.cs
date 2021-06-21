using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DoIt.Domain;
using DoIt.Domain.Common;
using DoIt.Domain.TodoListAggregate;
using MediatR;

namespace DoIt.Application.ToDos
{
    public class AddTodoItemCommand:IRequest<TodoItemDto>
    {
        public AddTodoItemCommand(Guid listId, string title)
        {
            ListId = listId;
            Title = title;
        }

        public Guid ListId { get;  }
        public string Title { get; }
    }
    
    public class AddTodoItemCommandHandler:IRequestHandler<AddTodoItemCommand,TodoItemDto>
    {
        private readonly IRepository<TodoList, Guid> _repository;
        private readonly IMapper _mapper;

        public AddTodoItemCommandHandler(IRepository<TodoList,Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<TodoItemDto> Handle(AddTodoItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetByIdAsync(request.ListId);
            var todoId = list.AddToDo(request.Title);

            await _repository.UpdateAsync(list);

            return _mapper.Map<TodoItemDto>(list.Items.Single(x => x.Id == todoId));
        }
    }
}