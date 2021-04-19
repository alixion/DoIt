using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DoIt.Domain;
using DoIt.Domain.Common;
using DoIt.Domain.TodoListAggregate;
using MediatR;

namespace DoIt.Application.ToDos
{
    public class AddTodoListCommand:IRequest<TodoListDto>
    {
        public AddTodoListCommand(string title)
        {
            Title = title;
        }

        public string Title { get;  }
    }
    
    public class AddTodoListCommandHandler:IRequestHandler<AddTodoListCommand,TodoListDto>
    {
        private readonly IRepository<TodoList, Guid> _repository;
        private readonly IMapper _mapper;

        public AddTodoListCommandHandler(IRepository<TodoList,Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<TodoListDto> Handle(AddTodoListCommand request, CancellationToken cancellationToken)
        {
            var list = new TodoList(request.Title);
            var createdItem = await _repository.AddAsync(list);
            
            return _mapper.Map<TodoListDto>(createdItem);
        }
    }
}